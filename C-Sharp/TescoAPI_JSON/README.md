# TESCO API
This project demonstrates the Tesco web API, and the use of JSON to interpret the XML results.

The TESCO API (https://devportal.tescolabs.com/) allows developers to search the Tesco grocery product list for information such as product name, price and image.

To compile the program (1) Set up a developer account from https://devportal.tescolabs.com/. Request an Ocp-Apim-Subscription-Key and update the line <code>"client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ocp key goes here");"</code> with your key. (2) Install the Netwonsoft JSON library in Visual Studio. Click on Tools-Nuget Package Manager-Package Manager Console, and then type "Install-Package Newtonsoft.Json".

To use the example program type in the name of an item of grocery item such as BANANA and click "Search".

This project also demonstrates
    (1)  how the windows cursor may be changed, 
    (2)  how the default button may be selected, 
    (3)  how to use a picturebox, 
    (4)  how to use a numericupdown, 
    (5)  how to popuplate a datagridview, 
    (6)  how to set the selection mode of a datagridview to "full row", 
    (7)  how to set the column mode of a datagridview to "auto size", 
    (8)  how to act on a selection change in a datagridview, 
    (9)  how to enable and disable a button, 
    
