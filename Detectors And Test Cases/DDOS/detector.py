# INVALID REFUND DDOS
from slither.slither import Slither 
from slither.core.cfg.node import NodeType 
  
def detect():
  slither = Slither('victimContract.sol')
  for contract in slither.contracts:
    entry_point = contract.get_function_from_signature("userBid()")
    assert entry_point
    var_refund = contract.get_state_variable_from_name("refunded")
    functions_reading_a = contract.get_functions_reading_from_variable(var_refund)
    function_using_a_as_condition = [
        f
        for f in functions_reading_a
        if f.is_reading_in_conditional_node(var_refund) or f.is_reading_in_require_or_assert(var_refund)
    ]
    print(f'The function using "refunded" in condition are {[f.name for f in function_using_a_as_condition]}')

detect()


  