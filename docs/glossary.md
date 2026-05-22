# ForgeERP Glossary (Ubiquitous Language)

## How to read this
Each term is the exact word used in code, APIs, and conversations.
No synonyms — if the glossary says "Item", nobody says "Product" or "Material".

## Item
Anything the business needs to track in the system. It can be a raw material 
you buy from a supplier, a component you manufacture in-house, or a finished 
good you sell to customers. Every Item has a unique code and a unit of measure.
Example: "FRAME-AL-M" is an aluminum frame measured in pieces (PCS). 
"TIRE-26" is a 26-inch tire. "BIKE-MTN" is a finished mountain bicycle.

## BOM (Bill of Materials)
A recipe or structure definition for manufacturing a product. 
It describes which components and quantities are needed to produce a finished item.
A BOM belongs to one finished Item.

Example:
The BOM for "BIKE-MTN" may include:
- 1 Frame
- 2 Tires
- 1 Handlebar


## BOM Line
A single component entry inside a BOM. 
Each BOM Line specifies which Item is required and in what quantity.

Example:
BOM: BIKE-MTN
Line:
- Item: TIRE-26
- Quantity: 2 PCS


## Routing
A sequence of manufacturing steps required to produce an item. 
It defines the workflow and the order of operations on the shop floor.

Example:
Routing for "BIKE-MTN":
1. Assemble frame
2. Install wheels
3. Install brakes
4. Final inspection


## Operation
One individual step within a Routing. 
An operation describes a specific task performed during production.

Example:
Operation: "Install Wheels"
- Work Center: Assembly Station
- Duration: 10 minutes
- Description: Attach front and rear tires to the bicycle frame.