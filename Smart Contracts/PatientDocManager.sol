pragma solidity ^0.6.4;
pragma experimental ABIEncoderV2;
contract RelationshipManager {
    mapping(address => AddressContainer) private ForDoc;
    mapping (address => Status) private ForPatient;
    address azero;
    struct AddressContainer{
        address one;
        address two;
        address three;
    }

    struct Status{
        uint status; // value : 0 => pending , 1 => accepted , 2 => rejected .
        address doc;   // address of the doctor.
    }
    constructor() public{
        azero = 0x0000000000000000000000000000000000000000;
    }
    //Check if doctor queue is full
    //returns true if there's place.
    function checkDoctorQueue(address doc) public view returns(bool){
        AddressContainer storage doc_queue = ForDoc[doc];
        if(doc_queue.one == azero || doc_queue.two == azero || doc_queue.three == azero) return true;
        return false;
    }
    //returns true if tried to already add doctor
    function checkDuplicateRequest(address doc) public view returns(bool){
        AddressContainer storage doc_queue = ForDoc[doc];
        if(doc_queue.one == msg.sender || doc_queue.two == msg.sender || doc_queue.three == msg.sender) return true;
        return false;
    }
    //returns true if a patient can add a doctor;
    function checkPossibility()public view returns (bool){
        Status storage patient_status = ForPatient[msg.sender];
        if(patient_status.doc == azero)return true;
        return false;
    }
    // Patient Only
    function add(address doc) public {
        AddressContainer storage doc_queue = ForDoc[doc];
        Status storage patient_status = ForPatient[msg.sender];
        if(doc_queue.one == azero){
            doc_queue.one = msg.sender;
            patient_status.doc = doc;
            patient_status.status = 0;
            return ;
        }
        if(doc_queue.two == azero){
            doc_queue.two = msg.sender;
            patient_status.doc = doc;
            patient_status.status = 0;
            return ;
        }
        if(doc_queue.three == azero){
            doc_queue.three = msg.sender;
            patient_status.doc = doc;
            patient_status.status = 0;
            return ;
        }
    }
    function getDoc(uint index) public view returns (address){
        if(index == 0){
            return ForDoc[msg.sender].one;
        }
        if(index == 1){
            return ForDoc[msg.sender].two;
        }
        if(index == 2){
            return ForDoc[msg.sender].three;
        }
    }
    // Doctor Only
    function respond(address patient, uint status) public {
        Status storage patient_status = ForPatient[patient];
        if(patient_status.doc!=msg.sender)return;
        if(patient_status.status != 0) return; //already responded;
        patient_status.status = status;
        AddressContainer storage doctor_queue = ForDoc[msg.sender];
        if(doctor_queue.one == patient){
            doctor_queue.one = azero;
        }
        if(doctor_queue.two == patient){
            doctor_queue.two = azero;
        }
        if(doctor_queue.three == patient){
            doctor_queue.three = azero;
        }
    }
//Patient Only
    function getPatient() public view  returns (address,uint) {
        Status storage patient_status = ForPatient[msg.sender];
        return (patient_status.doc,patient_status.status);
    }
    //Patient Only
    function updatePatient() public {
        Status storage patient_status = ForPatient[msg.sender];
        if(patient_status.status != 0){
            patient_status.doc = azero;
            patient_status.status = 0;
        }
    }
}