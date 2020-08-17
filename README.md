# GIS Interface
Demonstrates a Simio AddIn that gets data from GIS APIs (only Bing and Google Maps are currently implemented) to create Simio model objects.
A form is used to prompt the user for source and destination cities. Bing Maps is then queried for a route, and Simio nodes and links are constructed from the data.

It also demonstrates (New! August 2020) how a file of origin/destination points can be used to query Bing/Google in a batch, store the results to a json file, and then use that json file to create Simio objects in the facility view.

Note: The users must go to Bing Maps/Google maps to get a free license key.
