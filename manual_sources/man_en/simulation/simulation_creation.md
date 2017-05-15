---
title: Electric Circuit Simulation Creation
---

To create an electric circuit simulation you will need to use the toolbox and workbench. You need to follow these steps:

1. Place a simulation element from the toolbox on to the workbench
1. Set the properties for the simulation elements
1. Connect the simulation elements with lines, that represent wires

# How to add electric components to the circuit?
To place a simulation element on to the workbench, follow these steps:

1. Press the left mouse button on the simulation element in the toolbar that you want to place on to the workbench.
1. Keep the felt mouse button pressed and drag the simulation element on to the workbench. The simulation element will follow your mouse.
1. When you are satisfied with the position of the simulation element, let go of the left mouse button. The simulation element will align itself with the grid on the workbench. A new simulation element is created that represent electric circuit component of the chosen type.

# How to move the electric components on the workbench?
After adding the simulation element, you can still change its position.

To do so, follow these steps:

1. Press the left mouse button on the simulation element on the workbench that you want to move.
1. Keep the felt mouse button pressed and drag the simulation element. It will follow your mouse.
1. When you are satisfied with the position of the simulation element, let go of the left mouse button. The simulation element will align itself with the grid on the workbench.

# Component collision
If you try to place two simulation element on top of each other, the element that you are trying to place on the top will place itself next to the element on the bottom. The exact position is determined by the parts of the elements that overlap. 

# Adjusting the Electric Components
Simulation elements that are create have their properties set to default values. To change the properties, follow these steps:

1. Left click the desired element to select it
1. Change the properties of the selected element in the properties window

The properties can be changed even while the simulation is running. The simulation will adjust itself according to the new values of the properties.

## Various Types of Adjustable Properties
There are several property types that are set differently. When changing the values there is no need to confirm the new value. The new value will be applied when you stop click out of the input field or press enter.

To adjust different property types follow these steps:

1. Integer value without limit - write the desired value in to the input field.
1. Integer value with limit - use the slider or write the desired value in to the input field.
1. Decimal value without limit - write the desired value in to the input field. Use period (".") as a decimal separator.
1. Decimal value with limit - use the slider or write the desired value in to the input field. Use period (".") as a decimal separator.
1. Boolean value (true/false) - use the checkbox to change the value.
    1. If the checkbox is checked, the value is "true".
    1. If the checkbox is unchecked, the value is "false".

# How to connect electric components?
On the workbench, the wires are represented with lines. You can only connect the simulation elements that are on the workbench. To connect two simulation elements (A and B) follow these steps:

1. Left click one of the connectors of the simulation element A.
1. Keep the left mouse button pressed and drag the mouse to the connector of the simulation element B, that you want to connect to simulation element A. The simulation elements A and B are now connected, and electric current will flow between them in the simulation.

# How to break the lines differently?
The lines break in the shape of the letter L.

To break the line differently, follow these steps:

1. Left click the line, that you want to break to select it.
1. Press the spacebar on your keyboard.

# How to disconnect electric components?
To disconnect two simulation elements, you have to delete the line that connects them.

To do delete a line, follow these steps:

1. Left click the line, that you want to delete to select it.
1. Press the delete button on your keyboard.

# How to generate HTML Export?
To generate HTML export of the workbench, click the cloud icon in the toolbar. This opens a file explorer, that enables you to chose a file system path to export the workbench to.
