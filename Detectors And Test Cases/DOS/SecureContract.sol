// SPDX-License-Identifier: MIT

pragma solidity ^0.8.2;

contract SecureContract {
    address payable highestBidder;
    uint256 public winnerBid;
    mapping(address => uint256) refunds;

    
    function userBid() external payable {
        require(msg.value > winnerBid);

        refunds[highestBidder] += winnerBid;

        highestBidder = payable(msg.sender);
        winnerBid = msg.value;
    }

    
    function getwinner() external view returns (address) {
        return highestBidder;
    }

    
    function withdrawRefund() external payable {
        require(refunds[msg.sender] > 0, "No refunds available");

        (bool success, ) = msg.sender.call{value: refunds[msg.sender]}("");
        require(success, "Failed to transfer refund");
    }
}

contract AttackerContract {
    SecureContract secureContract;

    constructor(SecureContract _secureContract) {
        secureContract = _secureContract;
    }

    /// Prevent this contract from taking ETH
    fallback() external payable {
        revert();
    }

    /// Place a bid to the vulnerable contract
    function placeBid() external payable {
        require(msg.value > secureContract.winnerBid());

        secureContract.userBid{value: msg.value}();
    }
}