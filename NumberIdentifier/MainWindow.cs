
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;

namespace NumberIdentifier
{
    [Serializable]
    public struct Data
    {
        public List<int> sizes;
        public List<double[,]> biases;
        public List<double[,]> weights;
    }
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        double divider = 255.0;
        const int SIZE = 28;
        ByNetwork network;
        double[,] input = new double[SIZE * SIZE, 1];
        double[,] colors = new double[SIZE, SIZE];

        MnistImage[] images;
        MnistImage[] testImages;

        bool mouse = false;
        Graphics graphics;

        private void MainWindow_Load(object sender, EventArgs e)
        {

            graphics = pictureBoxPaint.CreateGraphics();
        }

        private void loadImages()
        {
            if (images == null)
                images = MnistImage.LoadData("train-images.idx3-ubyte", "train-labels.idx1-ubyte", 60000);

            if (testImages == null)
                testImages = MnistImage.LoadData("t10k-images.idx3-ubyte", "t10k-labels.idx1-ubyte", 10000);
        }
        private void startLearning()
        {
            this.Invoke(new Action(() =>
            {
                label2.Text = "Starting ...";
            }));


            loadImages();

            this.Invoke(new Action(() =>
            {
                label2.Text = "Processing Images ...";
            }));

            List<Tuple<double[,], double[,]>> training = new List<Tuple<double[,], double[,]>>();

            foreach(MnistImage image in images)
            {
                double[,] label = new double[10,1];
                for(int i = 0; i < 10; i++)
                {
                    label[i,0] = 0;
                }

                label[image.label, 0] = 1;

                double[,] layer = new double[image.Layer.Length, 1];
                for(int i =0; i < image.Layer.Length; i++)
                {
                    layer[i, 0] = image.Layer[i] / divider;
                }

                training.Add(new Tuple<double[,], double[,]>(layer, label));
            }

            List<Tuple<double[,], double[,]>> test = new List<Tuple<double[,], double[,]>>();

            foreach (MnistImage image in testImages)
            {
                double[,] label = new double[10, 1];
                for (int i = 0; i < 10; i++)
                {
                    label[i, 0] = 0;
                }

                label[image.label, 0] = 1;

                double[,] layer = new double[image.Layer.Length, 1];
                for (int i = 0; i < image.Layer.Length; i++)
                {
                    layer[i, 0] = image.Layer[i] / divider;
                }

                test.Add(new Tuple<double[,], double[,]>(layer, label));
            }

            int epocs = (int)txtEpocs.Value;

            this.Invoke(new Action(() =>
            {
                progressBar1.Maximum = epocs;
            }));

            network.statusChange += onStatusChanged;


            this.Invoke(new Action(() =>
            {
                label2.Text = "Learning ...";
            }));

            network.SGD(training, epocs, (int)txtBatchSize.Value, (double) txtEta.Value, chkUseTestData.Checked ?  test : null);

            this.Invoke(new Action(() =>
            {
                this.UseWaitCursor = false;
                this.panel1.Enabled = true;
                this.panel2.Enabled = true;
                MessageBox.Show("DONE");

                try
                {
                    Data obj = new Data();
                    obj.biases = network.biases;
                    obj.weights = network.weights;
                    obj.sizes = network.sizes;

                    byte[] bytes;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        IFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(memoryStream, obj);
                        bytes = memoryStream.ToArray();
                    }

                    // Save the byte array to a file
                    File.WriteAllBytes("network.bin", bytes);
                }
                catch(Exception ex)
                {

                }


            }));

            

        }

        private void onStatusChanged(int index, int epochs, string msg)
        {
            this.Invoke(new Action(() =>
            {
                progressBar1.Value = index+1;
                label2.Text = msg;
            }));
        }

        private void Clear()
        {
            graphics.Clear(Color.Black);
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    colors[i, j] = 0.0;
                    input[i + j * SIZE, 0] = 0.0;
                }
            }
            label1.Text = "";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void pictureBoxPaint_MouseDown(object sender, MouseEventArgs e)
        {
            mouse = true;
            label1.Text = "";
        }

        //try to recoginze hand drawn image from user
        private void pictureBoxPaint_MouseUp(object sender, MouseEventArgs e)
        {
           
            mouse = false;
            int k = 0;
            Bitmap map = new Bitmap(SIZE, SIZE);
            Graphics g = Graphics.FromImage(map);

            int minX = SIZE;
            int minY = SIZE;

            int maxX = 0;
            int maxY = 0;

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if(colors[j, i] != 0.0)
                    {
                        minX = Math.Min(minX, j);
                        minY = Math.Min(minY, i);
                        maxX = Math.Max(maxX, j);
                        maxY = Math.Max(maxY, i);
                    }

                    if (colors[j, i] != 0)
                    {
                        int color = (int)(colors[j, i] * 255);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(color, color, color)), new RectangleF(j, i, 1, 1));
                    }
                   
                }
            }

            graphics.Flush();
            g.Flush();

            Bitmap adjustedMap = new Bitmap(SIZE, SIZE);
            Graphics adjustedG = Graphics.FromImage(adjustedMap);

            adjustedG.DrawImage(map, SIZE/2 - (maxX - minX)/2, SIZE / 2 - (maxY - minY) / 2, new RectangleF(minX, minY, maxX - minX, maxY - minY), GraphicsUnit.Pixel);

            Bitmap resizedMap = new Bitmap(SIZE, SIZE);
            Graphics resizedG = Graphics.FromImage(resizedMap);

            float adjustment = 2;
            resizedG.DrawImage(adjustedMap,
                new RectangleF(adjustment, adjustment, SIZE - (adjustment*2), SIZE - (adjustment * 2)), 
                new RectangleF(0, 0, SIZE, SIZE), GraphicsUnit.Pixel);

            pictureBox1.Image = resizedMap;
            pictureBox1.Refresh();

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Color c = resizedMap.GetPixel(j, i);

                    float value = ((c.R/255f) + (c.B/255f) + (c.G/255f))/3.0f;
                    input[k, 0] = value < 0.3f ? 0 : Math.Min(1f, value * 1.2f);
                    k++;
                }
            }

            var predict = network.FeedForward(input);

            var predictedForward = predict.Cast<double>().ToArray();
            var predictedForwardMax = predict.Cast<double>().ToArray().Max();

            int result = Array.IndexOf(predictedForward, predictedForwardMax);
            label1.Text = result + "";
        }

        private void pictureBoxPaint_MouseMove(object sender, MouseEventArgs e)
        {
            float pixelSize = 10f;
            if (mouse)
            {
                int mx = ((int)(e.X / pixelSize)), my = ((int)(e.Y / pixelSize));
                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        double dist = (i - mx) * (i - mx) + (j - my) * (j - my);
                        if (dist < 1) dist = 1;
                        dist *= dist;
                        colors[i, j] += (0.2 / dist) * 3;
                        if (colors[i, j] > 1) colors[i, j] = 1.0;
                        if (colors[i, j] < 0.035) colors[i, j] = 0.0;

                        int color = (int)(colors[i, j] * 255);
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(color, color, color)), i * pixelSize, j * pixelSize, pixelSize, pixelSize);
                    }
                }
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int index = (int) numericUpDown1.Value;

            MnistImage image = testImages[index];

            double[,] label = new double[10, 1];

            label3.Text = "Expected: " + image.label;

            double[,] layer = new double[image.Layer.Length, 1];
            for (int i = 0; i < image.Layer.Length; i++)
            {
                layer[i, 0] = image.Layer[i] / divider;
            }

            pictureBoxPaint.Image = MnistImage.GetBitmap(image, 1);

            var predict = network.FeedForward(layer);

            var predictedForward = predict.Cast<double>().ToArray();
            var predictedForwardMax = predict.Cast<double>().ToArray().Max();

            int result = Array.IndexOf(predictedForward, predictedForwardMax);
            label1.Text = result + "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>();

            foreach (string split in txtLayers.Text.Split(','))
            {
                list.Add(int.Parse(split));
            }
            //784, 250, 100, 10
            network = new ByNetwork(list,new SigmoidActivation());

            label4.Text = (DateTime.Now).ToString();
            this.UseWaitCursor = true;
            this.panel1.Enabled = false;
            this.panel2.Enabled = false;
            (new Task(() =>
            {
                this.startLearning();
            })).Start();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.pictureBoxPaint.Enabled = this.checkBox1.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!File.Exists("network.bin"))
            {
                MessageBox.Show("No network file exist");
                return;
            }

            this.UseWaitCursor= true;
            Data obj;
     
            byte[] bytes;
            using (var memoryStream = new MemoryStream(File.ReadAllBytes("network.bin")))
            {
                var formatter = new BinaryFormatter();
                obj = (Data)formatter.Deserialize(memoryStream);
            }

            loadImages();

            network = new ByNetwork(obj.sizes, new SigmoidActivation());
            network.weights = obj.weights;
            network.biases = obj.biases;

            label2.Text = "Network Loaded :)";
            this.UseWaitCursor = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(this.network == null)
            {
                MessageBox.Show("No network available please load one or train it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.loadImages();

            this.progressBar2.Value = 0;
            this.progressBar2.Maximum = testImages.Length;
            this.panel1.Enabled = false;
            this.panel2.Enabled= false;

            (new Task(() =>
            {

                int errors = 0;
                for(int index = 0; index < testImages.Length; index++)
                {

                    MnistImage image = testImages[index];

                  
                    double[,] layer = new double[image.Layer.Length, 1];
                    for (int i = 0; i < image.Layer.Length; i++)
                    {
                        layer[i, 0] = image.Layer[i] / divider;
                    }

                    var predict = network.FeedForward(layer);

                    var predictedForward = predict.Cast<double>().ToArray();
                    var predictedForwardMax = predict.Cast<double>().ToArray().Max();

                    int result = Array.IndexOf(predictedForward, predictedForwardMax);
                   
                    this.Invoke(new Action(() =>
                    {
                        this.progressBar2.Value = index;
                        label7.Text = $"Test {index+1}/{testImages.Length}";
                        if(result != (int)image.label)
                        {
                            errors++;
                            this.listView1.Items.Add(new ListViewItem($"@{index} Expected {image.label} Found {result}"));
                        }
                    }));
                }

                this.Invoke(new Action(() =>
                {
                    this.progressBar2.Value = this.progressBar2.Maximum;

                    double ration = Math.Round((double)errors / (double)testImages.Length, 2);

                    label7.Text = $"Err: {errors}, Suc: {testImages.Length - errors}, Err %: {ration}, Tot: {testImages.Length}";

                    this.panel1.Enabled = true;
                    this.panel2.Enabled = true;

                    MessageBox.Show("Done Testing");
                }));

            })).Start();
        }
    }
}
