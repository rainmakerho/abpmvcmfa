# 從零開始：在 ABP MVC 專案中實現多因素驗證 (MFA) 的完整教學

## About this solution

MFA（多因素驗證）是提升帳號安全性的重要手段，能有效防止因密碼洩漏而導致的未授權存取。然而，在 ABP Framework 中，若想使用官方完整支援的 MFA 功能，通常需要升級至 Pro 版本。

事實上，ASP.NET Core Identity 本身就內建了基本的 MFA 支援。若不追求 Pro 版那種進階與全面的安全機制，只想啟用一般的多因素驗證功能，其實只需透過少量設定與簡單的 UI 介面，就能達成大部分的實務需求。

本專案在 ABP MVC 中，加入啟用/停用 MFA 的相關功能。

### Pre-requirements

- [.NET9.0+ SDK](https://dotnet.microsoft.com/download/dotnet)
- [Node v18 or 20](https://nodejs.org/en)
- [ABP v9.1](https://abp.io/)

## 執行畫面

1. 使用者登入後，如果沒啟用 MFA ，則會被導到啟用 MFA 畫面
   <br/>
   <img src="https://rainmakerho.github.io/2025/04/28/abp-framework-enable-mfa/07.png">
   <br/>
   <img src="https://rainmakerho.github.io/2025/04/28/abp-framework-enable-mfa/02.png">

2. 使用者啟用 MFA 後，下次登入後，會需要輸入 MFA 驗證碼來登入驗證。
   <img src="https://rainmakerho.github.io/2025/04/28/abp-framework-enable-mfa/06.png">

### Additional resources

- [從零開始：在 ABP MVC 專案中實現多因素驗證 (MFA) 的完整教學](https://rainmakerho.github.io/2025/04/28/abp-framework-enable-mfa/)
