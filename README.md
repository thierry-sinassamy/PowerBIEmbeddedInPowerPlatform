# PowerBIEmbeddedInPowerPlatform

## Problem
In a Power Apps form within the Microsoft Power Platform, it is necessary to embed a Power BI report when we want to display one. From a technical standpoint, the Power Platform’s Dataverse stores data in XML format, which includes a link to the Power BI service—specifically pointing to the report intended for display.

The issue arises when the GUID of the Power BI report stored in the Dataverse SQL database no longer matches the GUID of the report currently deployed in the Power BI environment. 

When this mismatch occurs, the iframe fails to resolve the report correctly, resulting in a generic rendering error within the Power Apps form. This error typically appears during form load or refresh events, and provides little diagnostic information to the end user, making troubleshooting more complex.

To mitigate this, it is essential to implement validation mechanisms to ensure GUID consistency between the Dataverse and Power BI, or to design a dynamic lookup mechanism that retrieves the correct report ID at runtime based on metadata or report names.

 

In addition to this, there is another drawback of the Power Platform: it does not generate any events or error logs. As a result, it becomes nearly impossible to capture the occurrence of the error when the Power BI report fails to load in the iframe of the Power Apps form.

Also, on the Power BI Service side, it is not possible to capture an event when a modified report is deployed to a workspace. In fact, an event is generated, but it is only accessible to the Power BI Service administrator—that is, the administrator of the Power BI Admin Center.

<img width="1044" height="415" alt="image" src="https://github.com/user-attachments/assets/70e2c7f9-2da6-4308-93d6-a1fd4a47fd56" />

## Solution
The inability to capture an event on either platform—or to obtain an error log from Power Platform—becomes a critical factor in the decision-making process when choosing a solution.

To address this, a robust solution involves developing a background service that acts as a scheduled job rather than a real-time listener. This service would:

Periodically query the Dataverse to retrieve the current report references (GUIDs or URLs).

Validate these references against the actual reports available in the Power BI workspace using the Power BI REST API.

Identify mismatches between stored GUIDs and active reports.

Automatically update the Dataverse with the correct GUIDs or URLs when discrepancies are found.

This scheduled approach ensures consistency between the Dataverse and Power BI without introducing latency or complexity into the Power Apps runtime. It also avoids the overhead of implementing real-time event listeners or webhooks, which may not be necessary for this use case.

<img width="936" height="389" alt="image" src="https://github.com/user-attachments/assets/40add804-b522-4801-b332-794b4b4249e5" />
