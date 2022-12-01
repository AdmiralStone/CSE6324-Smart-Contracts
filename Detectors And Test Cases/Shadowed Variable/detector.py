from slither.detectors.abstract_detector import AbstractDetector, DetectorClassification
from slither.core.cfg.node import NodeType
import re

# INVALID RETURN VARIABLES
from slither.slither import Slither 
from slither.core.cfg.node import NodeType 
  
def detect():
    slither = Slither('irv.sol')  
    res = []
    for contract in slither.contracts:
        for function in contract.functions:
            for return_var in function.returns: #potentially shadower
                for local_var in function.variables: #potentially shadowed
                    if return_var.name == local_var.name:
                        res.append([local_var.name])
                    
    return res

print(f"SHADOWED VARIABLES: {detect()}")
