# KAI - Self Driving Car AI
An intelligent, self driving car agent using the Unity ML-Agents Library. The agent was trained using reinforcement learning (PPO algorithm), implemented in the library.
<br/>
<br/>


## Project summary
This is a deep reinforcement learning project made using the Unity ML-Agents library. Actions that the agent can take are accelerating/braking and steering. Its task is to navigate the track without colliding with a wall, while passing checkpoints distributed along the tracks and collecting rewards.
<br/>
<br/>


## Getting started
- clone the project
    ``` bash
    git clone https://github.com/mateja-filipovic/KAI
    ```
- create a conda environment with dependencies using the `kay_env.yml` file (make sure you have anaconda installed before this step)
    ``` bash
    conda env create -f kay_env.yml
    ```
<br/>


## Usage

### Pretrained models
- Pretrained models are available in the results folder
- `AgentOne` was trained on the first track, `Agent two` on the second and so on
- `AgentsCombined` is a brain trained using all three agents on their respective tracks, at the same time

### Model Training
- After creating and activating the conda environment, the models can be trained using the `mlagents-learn` command
- here is an example: `mlagents-learn <PATH_TO_CONFIG_FILE> --env=<EXECUTABLE> --no-graphics --run-id=<RUN_ID>`
    - replace the `<PATH_TO_CONFIG_FILE>` with a path to the learning configuration file
    - replace `<EXECUTABLE>` with the name of the unity executable containing agents
    - `<RUN_ID>` is a unique name specific to this training session
- for more information on how to use the `mlagents-learn` you can refer to **[mlagents-learn docs](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Training-ML-Agents.md)**
- for more information about the config file, you can refer to the **[training config file docs](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Training-Configuration-File.md)**

## Docs
Explanation of the project core can be found in the docs folder. It includes a description of some implementation details, namely actions and obeservations implementations. For library reference, use can use **[this link](https://github.com/Unity-Technologies/ml-agents/tree/main/docs)**


## Demo
Here's a quick demo of a pretrained model navigating the track:<br/>
https://user-images.githubusercontent.com/62219501/181906662-53b9a64f-7842-4f04-b7bf-c498c2d1456c.mov

<!-- URL DEFINITIONS -->
[Unity-Url]: https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white