# Jñāna — Clean Architecture Workspace
## A Vanara Companion Workbench

# Jñāna — Clean Architecture Workspace

![GitHub Repo](https://img.shields.io/badge/Code-Jñāna-blue?style=flat&logo=github)
![WinUI 3](https://img.shields.io/badge/UI-WinUI%203-0078D4?style=flat&logo=windows)
![Architecture](https://img.shields.io/badge/Style-Clean%20Architecture-orange?style=flat)
![Status](https://img.shields.io/badge/State-Active%20Development-brightgreen?style=flat)
![License: MIT](https://img.shields.io/badge/License-MIT-yellow?style=flat&logo=github)


Jñāna is a modular WinUI 3 workspace designed around clarity, structure, and predictable UI behavior. The current iteration focuses on a clean foundation: a unified navigation service, a consistent window model, and a lightweight shell that keeps complexity out of the core.

The Workbench concept defines how tools, editors, and panels coexist. Each component follows a strict separation of concerns: views remain thin, viewmodels handle state, and services provide the underlying mechanics. This keeps the system flexible while avoiding hidden coupling.

Navigation is fully centralized. Pages register themselves through feature tiles, keyboard shortcuts integrate directly with the navigation service, and the main window acts as a stable host. The goal is to make movement inside the app feel immediate and spatially coherent.

The QuickLaunchBar introduces a minimal docking panel for fast access to tools. It is intentionally simple: no heavy chrome, no nested UI, just a clean entry point for actions and modules. This aligns with the overall Jñāna philosophy of reducing visual noise.

The theme system provides consistent colors, glyphs, and system button styling. It ensures that every part of the UI feels like one product rather than a collection of unrelated pages. Branding elements follow the JetBrains‑inspired direction established earlier.

Jñāna is currently in active development, with the Workbench transformation, Markdown editor integration, and virtual shell concepts progressing. The project aims to become a cohesive environment for tools, diagnostics, and creative workflows.

----
