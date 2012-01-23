from operator import itemgetter
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

    def challenge3(self):
        fileObject = open('chars.txt')
        str = fileObject.read()
        fileObject.close()
        dict = {}
        for ch in str:
            if not dict.has_key(ch):
                dict.keys().append(ch)
                dict[ch] = 1
            else:
                dict[ch] += 1
        sortedByValue = sorted(dict.items(), key = itemgetter(1))
        filtered = filter(lambda x: x[1]<10, sortedByValue)
        return zip(*filtered)[0]

class PythonChallengeTests(TestCase):

    def test_challenge1(self):
        result = PythonChallenge().challenge1()
        self.assertEqual(274877906944, result)

    def test_challenge2(self):
        result = PythonChallenge().challenge2()
        self.assertEqual('ocr', result)

    def test_challenge3(self):
        result = PythonChallenge().challenge3()
        self.assertTrue('a' in str(result))
        self.assertTrue('e' in str(result))
        self.assertTrue('i' in str(result))
        self.assertTrue('l' in str(result))
        self.assertTrue('q' in str(result))
        self.assertTrue('t' in str(result))
        self.assertTrue('y' in str(result))
        self.assertTrue('u' in str(result))

        
            




