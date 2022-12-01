# Improper access control detector
from slither.slither import Slither 
from slither.core.cfg.node import NodeType 
  
def detect():
  slither = Slither('sabotage.sol')  
  ret = []

  for contract in slither.contracts:
    entry_point = contract.get_function_from_signature("shuthotel()")
    assert entry_point
    all_calls = entry_point.all_internal_calls()

    all_calls_formated = [f.name for f in all_calls]

    #  Print the result
    print(f"From entry_point the functions reached are {all_calls_formated}")
    for name in all_calls_formated:
      if 'selfdestruct' in name:
        print(f"Self Destruct Function Found")




detect()


  