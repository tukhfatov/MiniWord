<div align="center">
<p><a href="https://www.nuget.org/packages/MiniWord"><img src="https://img.shields.io/nuget/v/MiniWord.svg" alt="NuGet"></a>  <a href="https://www.nuget.org/packages/MiniWord"><img src="https://img.shields.io/nuget/dt/MiniWord.svg" alt=""></a>  
<a href="https://ci.appveyor.com/project/mini-software/MiniWord/branch/master"><img src="https://ci.appveyor.com/api/projects/status/b2vustrwsuqx45f4/branch/master?svg=true" alt="Build status"></a>
<a href="https://gitee.com/dotnetchina/MiniWord"><img src="https://gitee.com/dotnetchina/MiniWord/badge/star.svg" alt="star"></a> <a href="https://github.com/mini-software/MiniWord" rel="nofollow"><img src="https://img.shields.io/github/stars/mini-software/MiniWord?logo=github" alt="GitHub stars"></a> 
<a href="https://www.nuget.org/packages/MiniWord"><img src="https://img.shields.io/badge/.NET-%3E%3D%204.5-red.svg" alt="version"></a>
</p>
</div>

---

<div align="center">
<p><strong><a href="README.md">English</a> | <a href="README.zh-CN.md">简体中文</a> | <a href="README.zh-Hant.md">繁體中文</a></strong></p>
</div>

---

<div align="center">
 Your <a href="https://github.com/mini-software/MiniWord">Star</a> and <a href="https://miniexcel.github.io">Donate</a> can make MiniWord better 
</div>

---


### Introduction

MiniWord is simple .NET Word Template tool, you can use like react/angular/vue string format like "{{tag}}" to dynamic input .net data.

### Basic Template Export

```csharp
			var value = new Dictionary<string, object>()
			{
                ["Company_Name"] = "MiniSofteware",
                ["Name"] = "Jack",
				["CreateDate"] = new DateTime(2021, 01, 01),
				["VIP"] = true,
				["Points"] = 123,
				["APP"] = "Demo APP",
			};
			MiniSoftware.MiniWord.SaveAsByTemplate(path, templatePath, value);
```

Template:

![image-20220912165113527](C:\Users\Wei\AppData\Roaming\Typora\typora-user-images\image-20220912165113527.png)

Output:

![image](https://user-images.githubusercontent.com/12729184/189612248-dd9381de-bbb8-4c72-adec-ac8982f60f96.png)