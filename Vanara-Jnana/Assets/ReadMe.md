# Vanara Jñāna  

*A Knowledge Explorer for the Vanara Windows API Library*

Vanara Jñāna is an interactive, WinUI 3–based explorer for the **Vanara** library.  
It provides a unified interface to browse APIs, inspect Shell objects, explore samples, and understand how the Vanara toolkit maps to the Windows platform.

The goal of Jñāna is to make the Vanara ecosystem more accessible — for contributors, maintainers, and developers who want to understand the structure and capabilities of the library.

---

## ✨ Features

- **API Explorer**  
  Navigate Vanara namespaces, types, functions, constants, and structures.

- **Shell & File Explorer Integration**  
  Visualize Shell items, folders, PIDLs, and related COM structures using Vanara’s Shell32 bindings.

- **Samples Browser**  
  Browse runnable code samples demonstrating how to use Vanara APIs in real scenarios.

- **WinUI 3 Interface**  
  Modern, fluent UI with navigation, search, and responsive layout.

- **Extensible Architecture**  
  Designed to grow with the Vanara library — new modules can be added without restructuring the core.

---

## 🧱 Technology Stack

- **WinUI 3 / Windows App SDK 2.2**
- **.NET 8**
- **Vanara 5.0.0**
- **C#**
- **MVU/MVVM‑friendly architecture**

---

## 📁 Project Structure
### Root: ̀ Vanara Jñāna 
- App.xaml
- 📁 /Controls
- 📁 /Models
- 📁 /Pages
	- ApiExplorerPage.xaml
	- ShellExplorerPage.xaml
	- SamplesPage.xaml
	- SettingsPage.xaml
- 📁 /Services
- 📁 /ViewModels

---

## 🚀 Getting Started

### Prerequisites
- Windows 10+ 22h2 or later 
- Visual Studio 2022 (17.10+)
- Windows App SDK 2.2
- .NET 8 SDK  

### Build & Run

```powershell
git clone https://github.com/<your-repo>/Vanara.Jnana.git
cd Vanara.Jnana
dotnet build
dotnet run
```

# Roadmap

[ ] Full Shell namespace visualization

[ ] Integrated COM inspector

[ ] Live API search with fuzzy matching

[ ] Sample runner with output capture

[ ] Plugin system for community extensions

[ ] Themed UI (Light/Dark/Mica/Custom)

# 🤝 Contributing

Contributions are welcome!

Please open issues for bugs or feature requests, and submit pull requests for improvements.

If you want to add features, fix bugs, or improve documentation:

Fork the repository

Create a feature branch

Submit a Pull Request

Please follow the existing code style and include clear commit messages.

📜 License

This project is licensed under the MIT License.
See LICENSE for details.

----
old version:
----

## Introduction
This Repository is an example of [WinClassicSamples](https://github.com/dahall/WinClassicSamplesCS/) using the Vanara 
libraries in a modern `WinUi 3` on `WinAppSDK` environment.

The original WinClassicSamples repository is a collection of samples that demonstrate the use of the Windows API in C#.

The goal is to demonstrate the use of the Vanara libraries in a side-by-side model with full featured modern WinUi environment.

This repository is intended to be a reference for developers who want to use the Vanara libraries in their own projects, and 
to provide a starting point for those who want to learn how to use the Vanara libraries in a WinUI3 App.

#### Project Intent
Test and validate that the structures, methods and interfaces in Vanara using known code and outcomes.
Demonstrate the use of the Vanara libraries in a side-by-side model with the native Win32 API.

#### Getting Started
First, take a look at [Template Studio for WinUI (C#)](https://marketplace.visualstudio.com/items?itemName=TemplateStudio.TemplateStudioForWinUICs),
available through Visual Studio Marketplace:

> _Template Studio for WinUI accelerates the creation of new WinUI apps using a wizard-based UI._
>
> Projects created with this extension contain well-formed, readable code and incorporate the latest development features while implementing proven patterns and leading practices. The generated code includes links to documentation and TODO comments that provide useful insight and guidance for turning the generated projects into production applications.
>
> To get started, install the extension, then select the corresponding Template Studio project template when creating a new project in Visual Studio. Name your project, then click Create to launch the Template Studio wizard.


# Resources
- [Vanara Git](https://github.com/dahall/Vanara)
- [WinClassicSamples using ``Vanara and WinForms`` Git](https://github.com/dahall/WinClassicSamplesCS)
- [Official WinUI3 on Microsoft.com](https://docs.microsoft.com/en-us/windows/apps/winui/winui3/)
- [Native Microsoft WinClassicSamples on GitHub](https://github.com/Microsoft/Windows-classic-samples)
- [Template Studio for WinUI (C#) on VisualStudio Marketplace](https://marketplace.visualstudio.com/items?itemName=TemplateStudio.TemplateStudioForWinUICs)