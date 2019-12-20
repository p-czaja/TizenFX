#!/usr/local/bin/python3.8

import sys
import re
import os
from pathlib import Path

SCRIPT_DIR = os.path.dirname(os.path.realpath(__file__)) 

class RpmDependencies():
        """Loads csapi-tizenfx rpm's native dependencies"""

        packaging = 'packaging'
        dllList = 'PlatformFileList.txt'
        librariesDeclarationsFile = 'Interop.Libraries.cs'
        specFileName = 'csapi-tizenfx.spec'

        def __init__(self):
            self.repoPath = os.path.dirname(os.path.dirname(SCRIPT_DIR))
            self.profiles = self.__getProfiles()
            self.allDlls = self.getAllDlls()
            self.profileDlls = {}
            self.libsDeclFiles = self.__findAllFiles(self.librariesDeclarationsFile)
            self.dllsLibs = {}
            self.__findDllsLibs()
            self.profileLibs = {}

        def __getProfiles(self):
            pattern = re.compile('#[a-z-]+')
            profiles = set()
            with open(os.path.join(self.repoPath, self.packaging, self.dllList)) as reader:
                for line in reader:
                    for match in pattern.finditer(line):
                        if match:
                            profile = match.group().replace('#', '')
                            profiles.add(profile)
            profilesList = list(profiles)
            profilesList.sort()
            return profilesList

        def getAllDlls(self):
            pattern = re.compile('^(.+dll).+')
            dlls = set()
            with open(os.path.join(self.repoPath, self.packaging, self.dllList)) as reader:
                for line in reader:
                    for match in pattern.finditer(line):
                        if match:
                            dll = match.group(1)
                            dlls.add(dll)
            dllsList = list(dlls)
            dllsList.sort()
            return dllsList

        def __getProfileDlls(self, profile):
            pattern = re.compile('^(.+dll).+' + profile + '\s')
            dlls = set()
            with open(os.path.join(self.repoPath, self.packaging, self.dllList)) as reader:
                for line in reader:
                    for match in pattern.finditer(line):
                        if match:
                            dll = match.group(1)
                            dlls.add(dll)
            dllsList = list(dlls)
            dllsList.sort()
            return dllsList

        def __getLibNames(self, dll):
        # TODO validate file format:
        #     fileFormatValidationPattern =re.compile(r"^\/\*\s+(\*.*\s*)+\*\/\s*internal static partial class Interop\s*{\s*internal .* Libraries\s*{.*")
        #     fileContent = ""
        #     with open(dll) as file:
        #         fileContent = file.read()
        #     print(fileContent)
        #     print(fileFormatValidationPattern.match(fileContent))
        #     if fileFormatValidationPattern.match(fileContent) is None:
        #         print(f'{dll} file doesn\'t meet the pattern')
        #         return None
            if dll == "":
                return list()
            pattern = re.compile('^\s*.*string.+\"(.+)\";')
            libs = list()
            with open(dll) as reader:
                for line in reader:
                    for match in pattern.finditer(line):
                        if match:
                            lib = match.group(1)
                            libs.append(lib)
            if not libs:
                print(f'{dll} file doesn\'t meet the pattern?')
            libs.sort()
            return libs
            
        def __findAllFiles(self, fileName):
            files = list()
            for file in Path(os.path.join(self.repoPath, 'src')).rglob(fileName):
                files.append(file)
            files.sort()
            return files

        def __mapDllToFile(self, dll):
            found = False
            for file in self.libsDeclFiles:
                dll2 = str(dll).replace('.dll', '')
                dirName1 = os.path.dirname(file)
                dirName2 = os.path.dirname(dirName1)
                moduleDirName = os.path.basename(dirName2)
                if dll2 == moduleDirName:
                    found = True
                    return str(file)
            if not found:
                return ""

        def __findDllsLibs(self):
            dllLibsFile = {}
            for dll in self.allDlls:
                dllFile = self.__mapDllToFile(dll)
                dllLibsFile[dll] = dllFile
                self.dllsLibs[dll] = self.__getLibNames(dllFile)

        def Load(self):
            for profile in self.profiles:
                if profile == 'ivi':
                    continue;
                self.profileDlls[profile] = self.__getProfileDlls(profile)
                libs = set()
                for dll in self.profileDlls[profile]:
                    for lib in self.dllsLibs[dll]:
                        libs.add(lib)
                libsList = list(libs)
                libsList.sort()
                self.profileLibs[profile] = libsList

        def UpdateSpecFile(self):
            for profile in self.profiles:
                if profile == 'ivi':
                    continue;
                with open(os.path.join(self.repoPath, self.packaging, self.specFileName)) as reader:
                   origSpec = reader.readlines() 

                modifiedSpec = self.__addProfileDeps(profile, origSpec)

                with open(os.path.join(self.repoPath, self.packaging, self.specFileName), 'w') as writer:
                    for line in modifiedSpec:
                        writer.write(line)

        def __addProfileDeps(self, profile, origSpec):
            lineToAdd = 'Requires:\t' + ' '.join(self.profileLibs[profile]) + '\n'
            origSpecList = list(origSpec)
            addLine = False
            pattern = re.compile('^%package\s+' + profile)
            pattern2 = re.compile('^Requires:\s*.*$')
            for i, line in enumerate(origSpecList):
                for match in pattern.finditer(line):
                    if match:
                        addLine = True
                for match in pattern2.finditer(line):
                    if match and addLine:
                        origSpecList.insert(i + 1, lineToAdd)
                        return origSpecList

def main(argv):
    try:
        dependencies = RpmDependencies()
        dependencies.Load()
        dependencies.UpdateSpecFile()
    except:
        print(f'Error: {sys.exc_info()[1]}')
    else:
        pass

if __name__ == '__main__':
    main(sys.argv[1:])
