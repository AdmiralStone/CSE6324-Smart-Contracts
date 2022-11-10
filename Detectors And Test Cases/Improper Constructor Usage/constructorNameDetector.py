import sys
from slither.slither import Slither
from slither.detectors.abstract_detector import AbstractDetector, DetectorClassification
from slither.core.cfg.node import NodeType
  
# Init slither
slither = Slither('hotelBooking_imoroper_constructor.sol')

#Iterate over all contracts in .sol file
for contract in slither.contracts:  
    print(f"Contract: {contract.name}")


Error = False
#Iterate over all functions in the contract a
for function in contract.functions:
    if(function.name == contract.name):
        print("Improper Initialization- Improper Constructor Usage")

