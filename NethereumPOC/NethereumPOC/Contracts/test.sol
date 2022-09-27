// SPDX-License-Identifier: GPL-3.0-or-later
pragma solidity ^0.8.17;
contract test {
    int _multiplier;

    // function test(int multiplier) public {
    //     _multiplier = multiplier;
    // }
    constructor(int multiplier) {                 
        _multiplier = multiplier;      
    } 
    function multiply(int val) public payable returns (int d) {
        return val * _multiplier;
    }
}