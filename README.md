# Power BI Embedder for [XrmToolBox](http://www.xrmtoolbox.com)
XrmToolBox plugin that allows you to embed the Power BI report into the CDS form.

This **ONLY** works for v9.0 and above!

## Preview

![](C:\Users\ivanf\Source\Repos\PowerBiEmbedder\docs\images\PBE-example.gif)

## Usage

How to use it guide can be found [here](https://dynamicsninja.blog/2019/12/05/power-bi-embedder-for-xrmtoolbox/).

## Creating Azure App Registration

Connection to Power BI API is achieved via Azure App Registration.

First you need to open Azure and create  App registration.

![](C:\Users\ivanf\Source\Repos\PowerBiEmbedder\docs\images\app-registration.png)

Click on the New registration and fill out the name of your app, leave everything else as default.

Look out for Application ID and Tenant ID on the overview tab and save them for later use.

![](C:\Users\ivanf\Source\Repos\PowerBiEmbedder\docs\images\appid-tenantid.png)

Next you need to add some dummy Redirect URI by clicking on the **Add a Redirect URI** on the right of your screen.

![](C:\Users\ivanf\Source\Repos\PowerBiEmbedder\docs\images\add-redirect-uri.png)

Click on the **Add a platform** button and **Desktop applications** Web on the right popup.

![](C:\Users\ivanf\Source\Repos\PowerBiEmbedder\docs\images\dummy-uri.png)

Input the random URI that you will use later. This URI can be a dummy one that is not real.

![](C:\Users\ivanf\Source\Repos\PowerBiEmbedder\docs\images\pbi-premissions.png)

Last thing you need to do is add Power BI Service permission by clicking on the **Add a permission** on the **API permission** tab and picking a **Power BI Service** option on the right.

![](C:\Users\ivanf\Source\Repos\PowerBiEmbedder\docs\images\select-permissions.png)

Select  **Delegated** option and pick 3 permissions: **Report.Read.All**, **Group.Read** & **Workspace.Read.All**. Finish the process by clicking on the **Add permission** button.

## Connect to Power BI API

Now we need 3 parameters from the previous step:

- **Application ID**
- **Tenant ID**
- **Redirect URI**

Put those parameters in the fields that appear after you click on the **Connect to PBI** button in the tool.