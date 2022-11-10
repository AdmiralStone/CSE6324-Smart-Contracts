from slither.detectors.abstract_detector import AbstractDetector, DetectorClassification
from slither.core.cfg.node import NodeType

class detector:
    def detect_omitted(self, return_vars, function):
            omitted_pairs = []
            for node in function.nodes:
                if node.type==NodeType.RETURN and node.variables_read!=return_vars:
                    omitted_pairs.append([return_vars, node])
            return omitted_pairs