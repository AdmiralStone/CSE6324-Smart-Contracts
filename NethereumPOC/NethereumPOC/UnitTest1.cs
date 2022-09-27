using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.IpcClient;
using Nethereum.Web3;
using Org.BouncyCastle.Math.EC.Multiplier;
using System;
using System.Transactions;

namespace NethereumASE
{
    /// <summary>
    /// Log Reports Command: dotnet test -l:trx;LogFileName=C:\Users\Deepak Prakash\source\repos\NethereumPOC\NethereumPOC\TestOutput.xml
    /// Log Reports Command: dotnet test -l:trx;LogFileName=C:\Users\Deepak Prakash\source\repos\NethereumPOC\NethereumPOC\TestResults\TestOutput.xml
    /// </summary>
    public class UnitTest1
    {
        /// <summary>
        /// Shoulds the be able to deploy contract.
        /// </summary>
        [Fact]
        public static async Task ShouldBeAbleToDeployContract()
        {
            try
            {
                var senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
                var password = "password";

                var abi = @"[{""inputs"":[{""internalType"":""int256"",""name"":""multiplier"",""type"":""int256""}],""stateMutability"":""nonpayable"",""type"":""constructor""},{""inputs"":[{""internalType"":""int256"",""name"":""val"",""type"":""int256""}],""name"":""multiply"",""outputs"":[{""internalType"":""int256"",""name"":""d"",""type"":""int256""}],""stateMutability"":""payable"",""type"":""function""}]";
                var byteCode = "0x608060405234801561001057600080fd5b5060405161014038038061014083398101604081905261002f91610037565b600055610050565b60006020828403121561004957600080fd5b5051919050565b60e28061005e6000396000f3fe608060405260043610601c5760003560e01c80631df4f144146021575b600080fd5b6030602c3660046054565b6042565b60405190815260200160405180910390f35b60008054604e90836082565b92915050565b600060208284031215606557600080fd5b5035919050565b634e487b7160e01b600052601160045260246000fd5b80820260008212600160ff1b84141615609b57609b606c565b8181058314821517604e57604e606c56fea2646970667358221220ae67f0cd9e5bf03dc4c5b1ebf314065509ef9f75dc4dad472a11643bdfa47fee64736f6c63430008110033";

                var multiplier = 7;
                // var web3 = new Web3.Web3("http://localhost:8545/");
                var web3 = new Web3Geth();
                var unlockAccountResult = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, password, 120/*new HexBigInteger(120)*/);
                Assert.True(unlockAccountResult);

                //web3.TransactionManager.DefaultGas = new HexBigInteger(3000000);
                //web3.TransactionManager.DefaultGasPrice = new HexBigInteger(3000000);

                var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(abi, byteCode, senderAddress, new HexBigInteger(250000), multiplier);

                var mineResult = await web3.Miner.Start.SendRequestAsync(6);

                //Assert.True(mineResult); --Not Required

                var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

                while (receipt == null)
                {
                    Thread.Sleep(5000);
                    receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
                }

                var contarctAddress = receipt.ContractAddress;

                var contract = web3.Eth.GetContract(abi, contarctAddress);

                var multiplyFunction = contract.GetFunction("multiply");

                var result = await multiplyFunction.CallAsync<int>(7);

                Assert.Equal(49, result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);              
            }
        }
    }
}