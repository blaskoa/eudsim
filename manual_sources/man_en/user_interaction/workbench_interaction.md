---
title: Workbench Interaction
---

{% include base.html %}

# Component Selection
You can select a component on the workbench which will grant you access to the interaction elements for the selected component. The enabled interaction elements in the main toolbar will change their colors to indicate the change.

To select a component:

1. Press left mouse button on the component you want to select. The selected component will be highlighted depending on the type of the component:
    * Electronic Components will have a square drawn around them.
    * Cables will change their color.
1. Press and hold left mouse button then drag the selection box to encompass the component you want to select.

# Selection of Multiple Components
To select multiple components press and hol left mouse button then drag it to draw the selection box. Anything inside the selection box will be selected.

The selected components can then be interacted with as with a single component, including the deletion, rotation and moving of the selection.

To deselect the selected components press left mouse button on an empty space of the workbench.

# Selection Movement
To move the selected component(s) you can do one of the following:

1. Press and hold left mouse button on any of the selected components and drag the mouse. All selected components will follow the mouse cursor.
1. Use keyboard shortcuts W, A, S, D to move the selection by one square in the respective direction.

# Component rotation
To rotate the selected component(s) you can do one of the following: 

1. Use keyboard shortcuts
1. Using the toolbar buttons.

It is possible to rotate multiple components at one. The components will rotate around the center point of the component group. The application automatically detects, if it should rotate one component or multiple components.

# Component rotation using keyboard shortcuts
To rotate the selected component(s) using keyboard shortcuts do the following:

1. Select the component(s) that you want to rotate
1. Press the:
    1. Q to rotate left (counter-clockwise)
    1. E to rotate right (clockwise)

# Component rotation using toolbar
To rotate the selected component(s) using keyboard the toolbar do the following:

1. Select the component(s) that you want to rotate
1. Press the button on the toolbar:
    1. ![Rotate left - icon]({{ base }}/images/user_interaction/rotate_left_icon.png "Rotate left") to rotate left (counter-clockwise)
    1. ![Rotate right - icon]({{ base }}/images/user_interaction/rotate_right_icon.png "Rotate right") rotate right (clockwise)

# Delete components
If you want the delete the component(s) do the following:

1. Left click the component you want to delete to select it.
1. Do one of the four:
    1. Press the delete button
    1. Right click the component and select "Delete"
    1. In the edit menu select "Delete"
    1. Press the delete button in the toolbar

It is possible to delete multiple components at once by selecting them with selection box. To draw a selection box left click an empty space on the workbench a drag to draw it.

After deleting a component, all connected lines will be deleted as well.

# Duplicate components
If you want to duplicate a component(s) do the following:

1. Left click the component you want to duplicate to select it.
1. Do one of the two:
    1. Right click the component and select "Duplicate"
    1. In the edit menu select "Duplicate"

# Navigating the workbench
To move around on the workbench do the following:

1. Press the middle mouse button anywhere on the workbench.
1. Keep the middle mouse button pressed and move the mouse around in the direction you want to move. The navigation works in a way that simulates moving a "camera" that is above the workbench.
1. Let go of the middle mouse button to stop moving.

# Zooming the workbench in and out
You can zoom in or out on the workbench by doing one of the two:

1. Using the scrollwheel on the mouse:
    1. scroll up - zoom in
    1. scroll down - zoom out
1. Using the button on the toolbar:
    1. Press the ![Zoom in - icon]({{ base }}/images/user_interaction/zoom_in_icon.png "Zoom in") to zoom in.
    1. Press the ![Zoom out - icon]({{ base }}/images/user_interaction/zoom_out_icon.png "Zoom out") to zoom out.
    
# Undo and redo the changes on the workbench
You can undo and redo the following actions:

1. Creating a component or a group of components
2. Deleting a component or a group of components
3. Moving a component or a group of components
4. Rotating a component or a group of components
5. Deleting a line

To undo/redo the changes, press these button in the toolbar:

1. Press the ![Redo - icon]({{ base }}/images/user_interaction/undo_icon.png "Redo") to redo an action
2. Press the ![Undo - icon]({{ base }}/images/user_interaction/redo_icon.png "Undo") to undo an action
