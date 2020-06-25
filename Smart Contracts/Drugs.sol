pragma solidity ^0.6.4;
contract Drugs{
    address creator;
    mapping(uint => uint64) Registered_Drugs;
    constructor() public{
        creator = msg.sender;
    }
    function add(uint code, uint64 price_in_gwei)  public {
        if(msg.sender != creator) return;
        Registered_Drugs[code] = price_in_gwei;
    }
    function get(uint code) public  view returns (uint64){
        return Registered_Drugs[code];
    }
    function change_creator(address new_creator) public {
        if(msg.sender != creator) return ;
        creator = new_creator;
    }
}