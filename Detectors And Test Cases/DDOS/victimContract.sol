// SPDX-License-Identifier: MIT
pragma solidity ^0.8.2;

contract  VictimContract{
    uint256 public winnerBid;
    address payable highestBidder;
    bool refunded = false;
    
    function userBid() external payable {
        require(msg.value > winnerBid);
        
        require(highestBidder.send(winnerBid));
        if(address(highestBidder).balance == winnerBid){
            refunded = true;
        }
        assert(refunded);
        highestBidder = payable(msg.sender);
        winnerBid = msg.value;
    }
    function getoWner() external view returns (address) {
        return highestBidder;
    }
}

