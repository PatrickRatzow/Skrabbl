<template>
  <div class="p-3">
    <div>X: {{ mouseInfo.x }}, Y: {{ mouseInfo.y }}</div>
    <canvas id="draw-canvas" 
            height="500"
            width="500"
            @mousemove="draw"
            @mousedown="startDrawing"
            @mouseup="stopDrawing"
            @mouseout="stopDrawing"
    />
    <div>
      <ul class="is-flex">
        <li v-for="color of colors">
          <ColorButton 
              :key="color.backgroundColor"
              :name="color.name"
              :background-color="color.backgroundColor"
              :text-color="color.textColor || 'white'"
              @click="setColor"
          />
        </li>
      </ul>
    </div>
    <div class="mt-2">
      <ul class="is-flex">
        <li v-for="thickness of thicknesses">
          <SizeButton
              :key="thickness"
              :size="thickness"
              @click="setThickness"
          />
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import ColorButton from "@/components/canvas/ColorButton";
import SizeButton from "@/components/canvas/SizeButton";
export default {
  name: "Canvas",
  components: {
    ColorButton,
    SizeButton
  },
  data() {
    return {
      mouseInfo: {
        isDown: false,
        x: 0,
        y: 0
      },
      colors: [
        { name: "Black", backgroundColor: "black" },
        { name: "Red", backgroundColor: "#ff0000" },
        { name: "Blue", backgroundColor: "#0000ff" },
        { name: "Green", backgroundColor: "#00ff00" },
        { name: "Light Grey", backgroundColor: "#d5d5d5", textColor: "black" }
      ],
      thicknesses: [
          1,
          2,
          4,
          8,
          16,
          32,
          64
      ],
      stroke: {
        color: "black",
        thickness: 1
      },
      nodes: []
    }
  },
  methods: {
    setColor(color) {
      this.stroke.color = color;
    },
    setThickness(thickness) {
      this.stroke.thickness = thickness;
    },
    startDrawing(e) {
      this.mouseInfo.x = e.offsetX
      this.mouseInfo.y = e.offsetY
      this.mouseInfo.isDown = true
    },
    stopDrawing(e) {
      if (!this.mouseInfo.isDown)
        return
      
      this.mouseInfo.x = 0
      this.mouseInfo.y = 0
      this.mouseInfo.isDown = false
    },
    draw(e) {
      if (!this.mouseInfo.isDown)
        return
      
      const oX = e.offsetX
      const oY = e.offsetY
      this.drawLine(this.mouseInfo.x, this.mouseInfo.y, oX, oY)
      this.mouseInfo.x = oX
      this.mouseInfo.y = oY
    },
    drawLine(x1, y1, x2, y2) {
      const canvas = document.getElementById("draw-canvas")
      
      const ctx = canvas.getContext("2d")
      ctx.beginPath();
      ctx.strokeStyle = this.stroke.color;
      ctx.lineWidth = this.stroke.thickness;
      ctx.moveTo(x1, y1);
      ctx.lineTo(x2, y2);
      ctx.stroke();
      ctx.closePath();
    },
  }
}
</script>

<style scoped>
  canvas {
    border: 2px solid grey;
  }
  
  button {
    color: white;
    height: 40px;
    min-width: 70px;
    margin-right: 10px;
    font-size: 16px;
  }
</style>