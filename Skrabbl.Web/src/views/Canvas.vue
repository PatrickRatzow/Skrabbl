<template>
    <div>
        <span>{{x}}, {{y}},</span>
        <h1>Drawing with mousemove event</h1>
        <canvas id="myCanvas" height="500" width="500" @mousemove="draw" @mousedown="beginDrawing" @mouseup="stopDrawing"></canvas>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                x: 0,
                y: 0,
                isDrawing: false
            }
        },
        methods: {
            drawLine(x1, y1, x2, y2) {
                let c = document.getElementById("myCanvas");
                this.canvas = c.getContext('2d');

                let ctx = this.canvas;
                ctx.beginPath();
                ctx.strokeStyle = 'black';
                ctx.lineWidth = 1;
                ctx.moveTo(x1, y1);
                ctx.lineTo(x2, y2);
                ctx.stroke();
                ctx.closePath();
            },
            draw(e) {
                if (this.isDrawing) {
                    this.drawLine(this.x, this.y, e.offsetX, e.offsetY);
                    this.x = e.offsetX;
                    this.y = e.offsetY;
                }
            },
            beginDrawing(e) {
                this.x = e.offsetX;
                this.y = e.offsetY;
                this.isDrawing = true;
            },
            stopDrawing(e) {
                if (this.isDrawing) {
                    this.drawLine(this.x, this.y, e.offsetX, e.offsetY);
                    this.x = 0;
                    this.y = 0;
                    this.isDrawing = false;
                }
            }
        }
    }
</script>

<style scoped>
    canvas {
        border: 1px solid grey;
    }
</style>