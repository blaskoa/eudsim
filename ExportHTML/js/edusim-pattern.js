const EduSim = (function init() {
    // --- CACHE DOM ---
    const $summaryWrapper = document.getElementById('summary-value-wrapper');
    const $canvas = document.getElementById('canvas');

    // --- ATTRIBUTES ---
    const ctx = $canvas.getContext('2d');
    const cw = $canvas.width;
    const ch = $canvas.height;
    const hotspots = [];

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

        ctx.clearRect(0, 0, cw, ch);
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
    draw();
}());
