# Ml-Agents-Toolkit

Desenvolvendo cenários e testando o aprendizado de máquina através do ML-Agents toolki da Unity.
https://github.com/Unity-Technologies/ml-agents

# Versionamento

Para este projeto, foi utilizada a Release 21 do ML-Agents, o SDK C# do ML-Agents da Unity encontrado no Package Manager da Unity na versão 2.0.1, a versão 3.10.11 do Python, versão do Unity Hub 3.7.0, versão do editor Unity 2021.3.25f1, versão do PyTorch 2.2.2.

## Cenário
---
Neste cenário, o agente (cubo) deve se movimentar a fim de pegar um ponto de recompensa (esfera) sem encostar em nenhuma parede ou chegar ao limite de passos.

O ponto inicial do agente e do ponto de recompensa são aleatorizados ao início de cada episódio.

Foram realizadas uma versão em 2D e outra em 3D, onde foram utilizadas diferentes técnicas para melhor a eficácia do treinamento do modelo.

### Versão 3D
---
Na versão 3D, foram dadas as seguintes observações ao agente:
1- Posição do agente.
2- Posição do ponto de recompensa.
3- Distância do agente até o ponto de recompensa.
4- Vetor para o ponto de recompensa.

### Versão 2D
---
Na versão 2D, foram adicionadas algumas observações ao agente, contendo então:
1- Posição do agente.
2- Posição do ponto de recompensa.
3- Distância do agente até o ponto de recompensa.
4- Vetor para o ponto de recompensa.
5- Distâncias do agente até cada parede.
6- Vetores para as paredes.

### Resultados
---
Os resultados podem ser vistos na pasta results de cada versão, utilizando o comando tensorboard --logdir results no prompt de comando, enquanto estiver na pasta do projeto.