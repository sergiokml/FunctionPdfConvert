<br/>
<p align="center">
    <a href="" target="_blank">
        <img width="10%" src="https://symbols.getvecta.com/stencil_28/38_functions.09b75fbe38.svg" alt="Azure Function">
    </a>
</p>

<br/>
<p align="center">
    <a href="LICENSE.txt" target="_blank">
        <img src="https://img.shields.io/badge/License-MIT-yellow.svg" alt="GitHub license">
    </a>
    <a href="https://github.com/sulu/sulu/releases" target="_blank">
        <img src="https://img.shields.io/github/tag/sulu/sulu.svg" alt="GitHub tag (latest SemVer)">
    </a>   
    <a href="https://github.com/sulu/sulu/commits/2.x" target="_blank">
        <img src="https://img.shields.io/github/commit-activity/y/sulu/sulu.svg" alt="GitHub commit activity">
    </a>
    <a href="https://github.com/sulu/sulu/graphs/contributors" target="_blank">
        <img src="https://img.shields.io/github/contributors-anon/sulu/sulu.svg" alt="GitHub contributors">
    </a>
    <a href="https://packagist.org/packages/sulu/sulu" target="_blank">
        <img src="https://img.shields.io/packagist/dt/sulu/sulu.svg" alt="Packagist downloads">
    </a>
</p>
<br/>

Function Azure that allows to upload a file and download it as PDF format.
This application is not for converting an XML file using the standards for electronic invoices set by [Servicio de Impuestos Internos](https://www.sii.cl/).

### ✅&nbsp; Requirements

For deploy or test this tool you needs:

+ A subscription account [Office365](https://developer.microsoft.com/en-us/microsoft-365/dev-program).

### 🚀&nbsp; Usage

This function can be hosted on a server and called from a POST request, upload the file and wait for the result. The file is uploaded to the Sharepoint site (linked to OneDrive) and when downloaded, the [Graph](https://learn.microsoft.com/en-us/graph/overview) tool automatically converts it into a PDF document. 

These are the [supported](https://learn.microsoft.com/en-us/graph/api/driveitem-get-content-format?view=graph-rest-1.0&tabs=http#format-options) files:

<table aria-label="Table 3" class="table table-sm">
<thead>
<tr>
<th style="text-align: left;">Format value</th>
<th style="text-align: left;">Description</th>
<th>Supported source extensions</th>
</tr>
</thead>
<tbody>
<tr>
<td style="text-align: left;">pdf</td>
<td style="text-align: left;">Converts the item into PDF format.</td>
<td>csv, doc, docx, odp, ods, odt, pot, potm, potx, pps, ppsx, ppsxm, ppt, pptm, pptx, rtf, xls, xlsx</td>
</tr>
</tbody>
</table>

### 📫&nbsp; Have a question? Found a Bug? 

Feel free to **file a new issue** with a respective title and description on the the [FunctionPdfConvert/issues](https://github.com/sergiokml/FunctionPdfConvert/issues) repository.

### ❤️&nbsp; Community and Contributions

I think that **Knowledge Doesn’t Belong to Just Any One Person**, and I always intend to share my knowledge with other programmers, a voluntary monetary contribution or contribute ideas and/or comments to improve these tools would be appreciated.

<p align="center">
    <a href="https://www.paypal.com/donate/?hosted_button_id=PTKX9BNY96SNJ" target="_blank">
        <img width="15%" src="https://img.shields.io/badge/PayPal-00457C?style=for-the-badge&logo=paypal&logoColor=white" alt="Azure Function">
    </a>
</p>


### 📘&nbsp; License

All my repository content is released under the terms of the [MIT License](LICENSE.txt).