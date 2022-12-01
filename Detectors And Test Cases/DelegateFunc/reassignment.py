# INVALID REFUND DDOS
from slither.slither import Slither 
from slither.detectors.abstract_detector import AbstractDetector, DetectorClassification
from slither.slithir.operations import LowLevelCall, InternalCall
from slither.core.declarations import Contract
from slither.utils.output import Output
from typing import List
from slither.core.cfg.node import NodeType, Node

def detect_delegatecall_in_loop(contract):
    results= []
    for f in contract.functions_entry_points:
        if f.is_implemented and f.payable:
            delegatecall_in_loop(f.entry_point, 0, [], results)
    return results


def delegatecall_in_loop(
    node: Node, in_loop_counter: int, visited: List[Node], results: List[Node]
) -> None:
    if node in visited:
        return
    # shared visited
    visited.append(node)

    if node.type == NodeType.STARTLOOP:
        in_loop_counter += 1
    elif node.type == NodeType.ENDLOOP:
        in_loop_counter -= 1

    for ir in node.all_slithir_operations():
        if (
            in_loop_counter > 0
            and isinstance(ir, (LowLevelCall))
            and ir.function_name == "delegatecall"
        ):
            results.append(ir.node)
        if isinstance(ir, (InternalCall)):
            delegatecall_in_loop(ir.function.entry_point, in_loop_counter, visited, results)

    for son in node.sons:
        delegatecall_in_loop(son, in_loop_counter, visited, results)


slither = Slither('delegatefunc.sol')  
res = []

for contract in slither.contracts:
    values = detect_delegatecall_in_loop(contract)
    for node in values:
        func = node.function    
        print(f"Found Issue in Function: {func.name}")