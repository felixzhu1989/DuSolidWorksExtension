
<div align=center><img src="resources/toolbox.png" width="200"/></div>

![Github](https://img.shields.io/badge/Github-build-blue.svg?style=flat-square)
![Github](https://img.shields.io/badge/Nuget-v0.1.1-pink.svg?style=flat-square)

# Overview

[English](https://github.com/weianweigan/DuSolidWorksExtension) | ����

�����ܶ����õ�SolidWorks Interface����չ����.

���� https://github.com/Weingartner/SolidworksAddinFramework,
���������ߴ�����һ���ܺõ�SolidWorks�Ŀ����ܹ�,�������޷�����͸���,�������޸���д���γ��˴˿�.


## ��װ

ʹ��Nuget����������װ

```
PM> Install-Package Du.SolidWorks -Version 0.1.1
```

## ʹ��

### 1. ��������ռ�

```
using Du.SolidWorks.Extension;
using Du.SolidWorks.Math
```

### 2. �����������ʹ�þ��������չ������

* ����ʽ����������չ����

 ![](resources/equExtension.png)


```csharp
var doc = _addin.SwApp.ActiveDoc as ModelDoc2;

var equ = doc.GetEquationMgr().GetAllEqu().
          Where(p => p.GlobalVariable).Select(p => p.VarName);
```

----------------------------------------------------------------------------
----------------------------------------------------------------------------

* �Զ������Ե���չ����

![](resources/cusExtension.png)

```csharp
var doc = _addin.SwApp.ActiveDoc as ModelDoc2;

var dateProerty = doc.Extension.CustomPropertyManager[""].GetAllProperty()
                ?.Where(p => p.Value.Contains("����"))?.Select(p => p.Name);
```

## �ĵ� 

* �ĵ����ڱ�д��

* [See Auto Generated Document](https://weianweigan.github.io/DuSolidWorksExtension/)

  --Du.SolidWorks.Extension (������չ�����ڴ������ռ���)

* ����չ�Ľӿ��б�����

>> ![](resources\tree.png)