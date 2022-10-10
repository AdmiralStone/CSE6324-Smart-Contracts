# CSE6324-Smart-Contracts
CSE 6324- Group project for testing smart contracts.

NethereumPOC: this solution will contain all features and developments from iterations and not just POC execution.
SlitherPOC: this solution will contain all features and developments from iterations and not just POC execution.

Setup Requirements:
1. Require Visual Studio with all Nethereum packages (Nethereum Web 3, Nethereum Geth, Xunit)
2. Require Visual Studio Code with solidity compiler and Remix intergration
3. Require Remix, Slither, python version 3.8 and higher

Instructions:
1. After Cloning the solution, USe the contract in the respective folders.
2. Once you have the compiled code, use the Byte Code and ABI and replace in the repective classes in VS.
3. Run the Private Chain "startgeth.bat"
4. Keep break point at the desired output line and Run Tests with XUnit.
5. Change the name of the .sol file in the python code on line number 9 to the name of your .sol file.
6. To execute the python script use 'python testing.py'
7. To analyze the .sol file usig slither bu without python use 'slither file_name.sol'
