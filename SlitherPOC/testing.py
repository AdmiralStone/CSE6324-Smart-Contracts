# Code Reference: https://github.com/crytic/slither/wiki/Python-API (Official Slither Documentation)
# Only refered for consuming the contract.
# The contact is built new.

import sys
from slither.slither import Slither
  
# Init slither
slither = Slither('hotelBooking.sol')

#Iterate over all contracts in .sol file
for contract in slither.contracts:  
    print(f"Contract: {contract.name}")
    
#Iterate over all functions in the contract and print the variables in it
    for function in contract.functions:  
        print('Function: {}'.format(function.name))  
  
        print('\tRead: {}'.format([v.name for v in function.state_variables_read]))  
        print('\tWritten {}'.format([v.name for v in function.state_variables_written]))


