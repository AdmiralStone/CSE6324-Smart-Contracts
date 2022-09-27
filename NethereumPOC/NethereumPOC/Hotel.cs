using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.ABI.Util;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.IpcClient;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.Web3;
using Org.BouncyCastle.Math.EC.Multiplier;
using System;
using System.Numerics;
using System.Transactions;
using static Nethereum.Util.UnitConversion;
using static NethereumPOC.UnitConversion;
using EthUnit = NethereumPOC.UnitConversion.EthUnit;

namespace NethereumPOC
{
    /// <summary>
    /// Log Reports Command: dotnet test -l:trx;LogFileName=C:\Users\Deepak Prakash\source\repos\NethereumPOC\NethereumPOC\TestOutput.xml
    /// Log Reports Command: dotnet test -l:trx;LogFileName=C:\Users\Deepak Prakash\source\repos\NethereumPOC\NethereumPOC\TestResults\TestOutput.xml
    /// </summary>
    public class Hotel
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

                var abi = @"[{""inputs"":[],""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":false,""internalType"":""address"",""name"":""_occupant"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""_value"",""type"":""uint256""}],""name"":""Occupy"",""type"":""event""},{""inputs"":[{""internalType"":""uint256"",""name"":""value"",""type"":""uint256""}],""name"":""bookRoom"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""name"":""currentStatus"",""outputs"":[{""internalType"":""enum HotelRoom.Statuses"",""name"":"""",""type"":""uint8""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""owner"",""outputs"":[{""internalType"":""address payable"",""name"":"""",""type"":""address""}],""stateMutability"":""view"",""type"":""function""}]";
                var byteCode = "0x608060405234801561001057600080fd5b506000805460ff196101003302166001600160a81b03199091161790556102548061003c6000396000f3fe608060405234801561001057600080fd5b50600436106100415760003560e01c80635f49d3d8146100465780638da5cb5b1461006f578063ef8a92351461009f575b600080fd5b61005961005436600461018f565b6100b9565b60405161006691906101a8565b60405180910390f35b6000546100879061010090046001600160a01b031681565b6040516001600160a01b039091168152602001610066565b6000546100ac9060ff1681565b60405161006691906101f6565b60008054600160ff19909116178082556040516060929182916101009091046001600160a01b03169085908381818185875af1925050503d806000811461011c576040519150601f19603f3d011682016040523d82523d6000602084013e610121565b606091505b5091509150600161013157600080fd5b60408051338152602081018690527fba951166d42eb077151bdecb7cf6027562f04f0143b0c141b1bb0a33eef5d693910160405180910390a15050604080518082019091526006815265109bdbdad95960d21b602082015292915050565b6000602082840312156101a157600080fd5b5035919050565b600060208083528351808285015260005b818110156101d5578581018301518582016040015282016101b9565b506000604082860101526040601f19601f8301168501019250505092915050565b602081016002831061021857634e487b7160e01b600052602160045260246000fd5b9190529056fea26469706673582212201e5ed9e18f8faa68d56f9581ce87aa6f519850a6ad801d8a04bdd37df3c00dfa64736f6c63430008110033";

                var web3 = new Web3Geth();
                var unlockAccountResult = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, password, 120/*new HexBigInteger(120)*/);
                Assert.True(unlockAccountResult);

                //web3.TransactionManager.DefaultGas = new HexBigInteger(3000000);
                //web3.TransactionManager.DefaultGasPrice = new HexBigInteger(3000000);

                var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(abi, byteCode, senderAddress, new HexBigInteger(250000));

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

                var hotelFunction = contract.GetFunction("bookRoom");

                #region Clean
                //BigInteger value = 1234;
                //EthUnit toUnit = EthUnit.Ether;

                //UnitConversion convert = new UnitConversion();
                //uint valNew = (uint)convert.FromWei(value,toUnit);
                #endregion

                var multipleEvent = contract.GetEvent("Occupy");

                var result = await hotelFunction.CallAsync<string>(1234);

                #region Cleanup
                //var filterAll = await multipleEvent.CreateFilterAsync();
                //var filterOne = multipleEvent.CreateFilterAsync(valNew);
                //var log = await multipleEvent.GetFilterChangesAsync<hotelEvent>(filterAll);
                //transactionHash = await hotelFunction.SendTransactionAsync(senderAddress, valNew);

                //receipt = await GetTransactionReceipt(web3, transactionHash);

                //var result = await hotelFunction.CallAsync<decimal>(valNew);
                #endregion

                Assert.Equal("Booked", result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #region Analysis
        //event Occupy(address _occupant, uint _value);
        //[FunctionOutput]
        //public class hotelEvent
        //{
        //    [Parameter("address","occupant",1,true)]
        //    public string Address { get; set; }

        //    [Parameter("int", "value", 2, true)]
        //    public int Value { get; set; }
        //}
        //public async Task<TransactionReceipt> MineAndGetReceiptAsync(Web3Geth web3, string transactionHash)
        //{
        //    var miningResult = await web3.Miner.Start.SendRequestAsync(200);
        //    Assert.True(miningResult);

        //    var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

        //    while (receipt == null)
        //    {
        //        Thread.Sleep(1000);
        //        receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
        //    }

        //    miningResult = await web3.Miner.Stop.SendRequestAsync();
        //    Assert.True(miningResult);
        //    return receipt;
        //}    
        #endregion
    }
}