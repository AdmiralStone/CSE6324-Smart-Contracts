// SPDX-License-Identifier: MIT

// Which version of solidity to use
pragma solidity ^0.8.0;

contract HotelRoom{
    enum Statuses {Vacant, Occupied}
    Statuses public currentStatus;

    event Occupy(address _occupant, uint _value);

    address payable public owner;

    constructor(){
        owner = payable(msg.sender);
        currentStatus = Statuses.Vacant;
    }

    modifier onlyWhileVacant(){
        require(currentStatus == Statuses.Vacant,"Currently Occupied.");
        _;
    }

    modifier cost(uint _amount){
        require(msg.value >= _amount,"Not enough ether provided"); // Silimar to assert in python
        _;
    }

    // function bookRoom() payable public onlyWhileVacant cost(2 ether) returns (uint val){
    //     currentStatus = Statuses.Occupied;
    //     // owner.transfer(msg.value)
    //     (bool sent, bytes memory data) = owner.call{value:msg.value}("");
    //     require(true);

    //     emit Occupy(msg.sender,msg.value);
    //     return msg.value;
    // }

    function bookRoom(uint value) public returns (string memory){
        currentStatus = Statuses.Occupied;
        // owner.transfer(msg.value)
        (bool sent, bytes memory data) = owner.call{value:value}("");
        require(true);

        emit Occupy(msg.sender,value);
        return "Booked";
    }
}