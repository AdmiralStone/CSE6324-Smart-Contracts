# https://github.com/crytic/slither/pull/1392

from slither.slithir.operations import (Binary,BinaryType)

def uInt_comparisions(contract):
    res = []

    for func in contract.functions_declared:
        nodeSet = set()
        variableSet = set()
        
        for node in function.nodes:
            for line in node.lines:
                if isinstance(line, Binary):
                    if (line.type == BinaryType.GREATER and line.variable_left.type.min == 0 and line.variable_right == 0):
                        nodeSet.add(node)
                        variableSet.add(line.variable_left)
                    if (line.type == BinaryType.LESS and line.variable_left == 0 and line.variable_right.type.min == 0):
                        nodeSet.add(node)
                        variableSet.add(line.variable_right)
                    if(line.type == BinaryType.EQUAL and line.variable_right != BinaryType.EQUAL):
                        nodeSet.add(node)
                        variableSet.add(line.variable_right)
                    
        res.append((function, nodeSet, variableSet))

        return res