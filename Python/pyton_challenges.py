import string
from unittest import TestCase
import math

class PythonChallenge:
    def challenge1(self):
        return math.pow(2, 38)

    def misc_challenge2(self):
        str = "g fmnc wms bgblr rpylqjyrc gr zw fylb. rfyrq ufyr amknsrcpq ypc dmp. bmgle gr gl zw fylb gq glcddgagclr ylb rfyr'q ufw rfgq rcvr gq qm jmle. sqgle qrpgle.kyicrpylq() gq pcamkkclbcb. lmu ynnjw ml rfc spj."
        intab = "abcdefghijklmnopqrstuvwxyz"
        outtab = "cdefghijklmnopqrstuvwxyzab"
        trantab = string.maketrans(intab, outtab)
        return str.translate(trantab)

    def challenge2(self):
        str = "map"
        intab = "abcdefghijklmnopqrstuvwxyz"
        outtab = "cdefghijklmnopqrstuvwxyzab"
        trantab = string.maketrans(intab, outtab)
        return str.translate(trantab)


class PythonChallengeTests(TestCase):

    def test_challenge1(self):
        result = PythonChallenge().challenge1()
        self.assertEqual(274877906944, result)

    def test_challenge2(self):
        result = PythonChallenge().challenge2()
        self.assertEqual('ocr', result)
