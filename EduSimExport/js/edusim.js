const EduSim = (function init() {
    // --- CACHE DOM ---
    const $summaryWrapper = document.getElementById('summary-value-wrapper');
    const $canvas = document.getElementById('canvas');

    // --- ATTRIBUTES ---
    const ctx = $canvas.getContext('2d');
    let hotspots = [{x:534.8,y:287.3,radius:50, rotate:0, img:'images/LedDiode.png' , componentName: '<p><span class="field-title">Current </span>0 [A] </p>'},{x:552.48,y:287.3,radius:7, img:'images/connector.png',componentName:'n/a'},{x:517.12,y:287.3,radius:7, img:'images/connector.png',componentName:'n/a'},{x:358,y:287.3,radius:50, rotate:0, img:'images/ZenerDiode.png' , componentName: '<p><span class="field-title">Current </span>0 [A] </p>'},{x:375.68,y:287.3,radius:7, img:'images/connector.png',componentName:'n/a'},{x:340.32,y:287.3,radius:7, img:'images/connector.png',componentName:'n/a'},{x:358,y:198.9,radius:50, rotate:0, img:'images/NPNTranzistor.png' , componentName: '<p><span class="field-title">Type </span> NPN </p>'},{x:358,y:182.2808,radius:7, img:'images/connector.png',componentName:'n/a'},{x:358,y:215.8286,radius:7, img:'images/connector.png',componentName:'n/a'},{x:338.5962,y:198.9,radius:7, img:'images/connector.png',componentName:'n/a'},{x:556.9,y:154.7,radius:50, rotate:0, img:'images/PNPTranzistor.png' , componentName: '<p><span class="field-title">Type </span>PNP </p>'},{x:556.9,y:138.0808,radius:7, img:'images/connector.png',componentName:'n/a'},{x:556.9,y:171.6286,radius:7, img:'images/connector.png',componentName:'n/a'},{x:536.4354,y:154.7,radius:7, img:'images/connector.png',componentName:'n/a'},{x:335.9,y:88.39997,radius:50, rotate:0, img:'images/accumulator.png' , componentName: '<p><span class="field-title">Voltage </span>10 [V] </p>'},{x:353.58,y:88.39997,radius:7, img:'images/connector.png',componentName:'n/a'},{x:318.22,y:88.39997,radius:7, img:'images/connector.png',componentName:'n/a'},];

    // Padding for circuit components
    const circuitComponentPadding = 50;

    // Get the key coordinates of the circuit
    const circuitRectangle = {};
    circuitRectangle.left = Math.min.apply(null, hotspots.map((h) => {
        if (Object.prototype.hasOwnProperty.call(h, 'z') && h.z < h.x) {
            return h.z;
        }
        return h.x;
    })) - circuitComponentPadding;

    circuitRectangle.top = Math.min.apply(null, hotspots.map((h) => {
        if (Object.prototype.hasOwnProperty.call(h, 'q') && h.q < h.y) {
            return h.q;
        }
        return h.y;
    })) - circuitComponentPadding;

    circuitRectangle.right = Math.max.apply(null, hotspots.map((h) => {
        if (Object.prototype.hasOwnProperty.call(h, 'z') && h.z > h.x) {
            return h.z;
        }
        return h.x;
    })) + circuitComponentPadding;

    circuitRectangle.bottom = Math.max.apply(null, hotspots.map((h) => {
        if (Object.prototype.hasOwnProperty.call(h, 'q') && h.q > h.y) {
            return h.q;
        }
        return h.y;
    })) + circuitComponentPadding;

    // Get middle coordinates for the canvas
    circuitRectangle.middle = {};
    circuitRectangle.middle.x = (circuitRectangle.right - circuitRectangle.left) / 2;
    circuitRectangle.middle.y = (circuitRectangle.bottom - circuitRectangle.top) / 2;

    // Set canvas to match the exported circuit
    ctx.canvas.width = circuitRectangle.right;
    ctx.canvas.height = circuitRectangle.bottom;

    // Get the middle coordinates of the canvas
    const canvasMid = {};
    canvasMid.x = Math.round($canvas.width / 2);
    canvasMid.y = Math.round($canvas.height / 2);

    // --- FUNCTIONS ---
    // Draw image to the canvas
    function drawImageRot(img, x, y, width, height, deg) {
        // Convert degrees to radian
        const rad = (deg * Math.PI) / 180;

        // Set the origin to the center of the image
        ctx.translate(x, y);

        // Rotate the canvas around the origin
        ctx.rotate(rad);

        // Draw the image
        ctx.drawImage(img, width / -2, height / -2, width, height);

        // Reset the canvas
        ctx.rotate(rad * (-1));

        //
        ctx.translate(-x, -y);
    }

    // Draw the canvas elements
    function draw() {
        for (let i = 0; i < hotspots.length; i += 1) {
            const h = hotspots[i];
            if (h.img.indexOf('wire.png') > 0) {
                ctx.beginPath();
                ctx.moveTo(h.x, h.y);
                ctx.lineTo(h.z, h.q);
                ctx.strokeStyle = 'black';
                ctx.closePath();
                ctx.stroke();
            } else if (h.img.indexOf('connector.png') > 0) {
                ctx.strokeStyle = 'black';
                ctx.beginPath();
                ctx.arc(h.x, h.y, h.radius, 0, 2 * Math.PI, false);
                ctx.closePath();
                ctx.fillStyle = 'red';
                ctx.fill();
                ctx.lineWidth = 1;
                ctx.strokeStyle = 'yellow';
                ctx.stroke();
            } else {
                ctx.strokeStyle = 'black';
                ctx.beginPath();
                ctx.arc(h.x, h.y, h.radius, 0, Math.PI * 2);
                ctx.closePath();
                ctx.stroke();
                const imageObj = new Image();
                imageObj.src = h.img;
                drawImageRot(imageObj, h.x, h.y, imageObj.width / 2, imageObj.height / 2, h.rotate);
                ctx.stroke();
            }
        }
    }

    // Write information about component
    function handleMouseMove(e) {
        // Tell the browser we're handling this event
        e.preventDefault();
        e.stopPropagation();

        /*
        getBoundingClientRect(): Returns the coordinates and size of the visible
        part of element - after resizing with CSS.
        - Take mouse position;
        - Take left and top coordinates from them;
        - Divide by visible width and height to get relative coordinates;
        - Multiply by canvas width and height (the defined ones);
        */
        const canvasRectangle = $canvas.getBoundingClientRect();
        const mouseX = (
            (e.clientX - canvasRectangle.left) / (canvasRectangle.width)
        ) * $canvas.width;
        const mouseY = (
            (e.clientY - canvasRectangle.top) / (canvasRectangle.height)
        ) * $canvas.height;

        ctx.clearRect(0, 0, $canvas.width, $canvas.height);
        draw();

        // Get data from Canvas and write it to the summary div
        let html = '';
        for (let i = 0; i < hotspots.length; i += 1) {
            const h = hotspots[i];
            const dx = mouseX - h.x;
            const dy = mouseY - h.y;
            if ((dx * dx) + (dy * dy) < h.radius * h.radius) {
                if (h.img.indexOf('connector.png') === -1 && h.img.indexOf('wire.png') === -1) {
                    html += h.componentName;
                    html += `<p>X:${h.x} Y:${h.y}</p>`;
                }
            }
        }
        $summaryWrapper.innerHTML = html;
    }

    // Run on mouse hover on canvas - write out information
    $canvas.onmousemove = ((e) => {
        handleMouseMove(e);
    });

    // --- RUN ON INIT ---
    // Move the circuit components to fit the canvas
    hotspots = hotspots.map((hotspot) => {
        const newHotspot = Object.assign({}, hotspot);
        newHotspot.x -= circuitRectangle.left;
        newHotspot.y -= circuitRectangle.top;
        newHotspot.x = (((newHotspot.x - circuitRectangle.middle.x)
            / circuitRectangle.middle.x) * canvasMid.x) + canvasMid.x;
        newHotspot.y = (((newHotspot.y - circuitRectangle.middle.y)
            / circuitRectangle.middle.y) * canvasMid.y) + canvasMid.y;
        if (Object.prototype.hasOwnProperty.call(newHotspot, 'z')) {
            newHotspot.z -= circuitRectangle.left;
            newHotspot.q -= circuitRectangle.top;
            newHotspot.z = (((newHotspot.z - circuitRectangle.middle.x)
                / circuitRectangle.middle.x) * canvasMid.x) + canvasMid.x;
            newHotspot.q = (((newHotspot.q - circuitRectangle.middle.y)
                / circuitRectangle.middle.y) * canvasMid.y) + canvasMid.y;
        }
        return newHotspot;
    });
    draw();
}());

const footer = (function init() {
    const $footer = document.getElementById('footer');
}());
