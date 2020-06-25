pragma solidity ^0.6.4;
contract Doctors{
    address creator;
    mapping(address => bool) Registered_doctors;
    constructor() public{
        creator = msg.sender;
    }
    function add(address doc)  public {
        if(msg.sender != creator) return;
        Registered_doctors[doc] = true;
    }
    function check(address doc) public  view returns (bool){
        return Registered_doctors[doc];
    }
    function change_creator(address new_creator) public {
        if(msg.sender != creator) return ;
        creator = new_creator;
    }
}