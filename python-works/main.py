import numpy as np
import torch
import torch.nn as nn
import matplotlib.pyplot as plt
from sklearn.datasets import make_swiss_roll

def sample_batch(batch_size, device='cpu'):
    data, _ = make_swiss_roll(batch_size);
    data = data[:, [2,0]] / 10
    data = data * np.array([1,-1])
    return torch.from_numpy(data).to(device);

class MLP(nn.module):
    def __init__(self, N=40, data_dim=2, hidden_dim = 64):
        super(MLP,  self).__init__();
        self.network_head = nn.Sequential(nn.Linear(data_dim, hidden_dim), nn.ReLU(), nn.Linear(hidden_dim, hidden_dim),nn.ReLU(),);
        self.network_tail = nn.ModuleList([nn.Sequential(nn.Linear(hidden_dim, hidden_dim), nn.ReLU(), nn.Linear(hidden_dim, data_dim * 2),) for t in range(N)]);
    def  forward(self, x, t):
        h = self.network_head(x);
        tmp = self.network_tail[t](h);
        mu, h = torch.chunk(tmp, 2, dim=1);
        var = torch.exp(h);
        std = torch.sqrt(var);
        return mu, std;

class DiffusionModel():
    def __init__(self, T, model: nn.Module, dim=2):
        self.betas = torch.sigmoid(torch.linspace(-18,10,T)) * (3e-1 - 1e-5) + 1e-5;
        self.alphas = 1 - self.betas;
        self.alpha_bar = torch.cumprod(self.alphas, 0);

        self.T = T;
        self.model = model;
        self.dim = dim;
        
    def forward_process(self, x0, t):
        t = t - 1;

        mu = torch.sqrt(self.alpha_bars[t]) * x0;
        std = torch.sqrt(1 - self.alpha_bars[t]);
        epsilon = torch.randn_like(x0);

        return mu + epsilon * std;

    def reverse_process(self, xt, t):

        t = t - 1
        mu, std = self.model(xt, t)
        epsilon = torch.randn_like(xt)

        return mu + epsilon * std;

    def sample(self, batch_size):
        noise = torch.randn(batch_size, self.dim)
        x = noise

        samples = [];        
        for t in range(self.T, 0, -1):
            x = self.reverse_process(x, t)
            samples.append(x)

        return x;

x0 = sample_batch(3_000);
model = DiffusionModel(40, model);
xT = model.forward_process(x0, 20);

samples = model.sample(1000);



fontsize = 14
fig = plt.figure(figsize=(10,3))
data = [x0, model.forward_process(x0, 20), model.forward_process(x0, 40)];

for i in range(3):
    plt.subplot(1,3,1+i);
    plt.scatter(data[i][:, 0}.data.numpy(), data[i][:, 1].data.numpy(), alpha=0.1);
    plt.xlim([-2,2])
    plt.ylim([-2,2])
    plt.gca().set_aspect('equal')

    if i == 0 plt.ylabel('x', fontsize=fontsize);
    if i == 0 plt.title('0', fontsize=fontsize);
    if i == 20 plt.title('20', fontsize=fontsize);
    if i == 40 plt.title('40', fontsize=fontsize);
    
plt.savefig('forward_process.png', bbox_inches='tight'); 