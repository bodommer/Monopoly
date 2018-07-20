import tkinter
import PIL
from PIL import Image, ImageDraw, ImageFont, ImageOps

quota = 119
width = 58

## create image and canvas
image = Image.new('RGBA', (760, 760), 'white')
canvas = ImageDraw.Draw(image)

## draw corner fields - basic lines
canvas.line([(0, quota), (760, quota)], fill='black')
canvas.line([(0, 760-quota), (760, 760-quota)], fill='black')
canvas.line([(quota, 0), (quota, 760)], fill='black')
canvas.line([(760-quota, 0), (760-quota, 760)], fill='black')

for i in (0, 2, 3, 5, 7, 8):
    if i != 3:
        canvas.line([(quota+i*width, quota-15), (quota+(i+1)*width, quota-15)], fill='black')    
        canvas.line([(quota-15, quota+i*width), (quota-15, quota+(i+1)*width)], fill='black') 
    canvas.line([(775-quota, quota+i*width), (775-quota, quota+(i+1)*width)], fill='black')
    if i != 7:
        canvas.line([(quota+i*width, 775-quota), (quota+(i+1)*width, 775-quota)], fill='black')
canvas.line([(quota+6*width, 775-quota), (quota+7*width, 775-quota)], fill='black')

## draw mid-board fields
for k in range(9):
    i = quota + k*width
    for j in (0, 760-quota):
        canvas.line([(i, j), (i, j+quota)], fill='black')
        canvas.line([(j, i), (j+quota, i)], fill='black')


## side border
canvas.rectangle([0,0,759, 759], outline='black', fill=None)
#canvas.line(((0,0), (760, 0)), fill='black')

## put field names to their positions

#canvas.setfont(font)



#canvas.text((20, 20), "Oracle", fill=(255, 255, 255, 255))


## colour same-group fields
image.save('image.png')
image = Image.open('image.png')
colors = ((255, 51, 51), (0, 128, 255), (152, 76, 0), (255, 153, 51), (255, 102, 255), (0, 0, 204), (0, 204, 102), (255, 255, 51))
points = [((quota + 3, quota -3), (quota+2*width+2, quota-3)),
          ((quota + 5 * width + 3, quota - 3), (quota + 7 * width + 3, quota - 3), (quota + 8 * width + 3, quota - 3)),
          ((763-quota, quota + 3), (763-quota, quota + 2*width + 3), (763-quota, quota + 3*width + 3)),
          ((763-quota, quota + 5*width + 3), (763-quota, quota + 7*width + 3), (763-quota, quota + 8*width + 3)),
          ((757-quota, 763-quota), (757-quota-2*width, 763-quota), (757-quota-3*width, 763-quota)),
          ((757-quota-5*width, 763-quota), (757-quota-6*width, 763-quota), (757-quota-8*width, 763-quota)),
          ((quota-3, 757-quota), (quota-3, 757-quota-1*width), (quota-3, 757-quota-3*width)),
          ((quota-3, 757-quota-6*width), (quota-3, 757-quota-8*width))]
for i in range(8):
    for j in range(len(points[i])):
        ImageDraw.floodfill(image, points[i][j], colors[i] )

# ("name", (location top-left), rotation (deg), font-size)
data = [("Oracle", (119, 0), 270, 32), ("SalesForce", (111+2*width, 0), 270, 22),
        ("Eclipse", (119+5*width, 0), 270, 32), ("Apache", (119+7*width, 0), 270, 32), ("JetBrains", (114+8*width, 0), 270, 26),
        ("W3", (689, 97), 0, 32), ("RedHat", (663, 119+2*width), 0, 32), ("Mozilla", (664, 122+3*width), 0, 32),
        ("Alibaba", (665, 121+5*width), 0, 28), ("Ebay", (676, 108+7*width), 0, 32), ("Amazon", (666, 120+8*width), 0, 28),
        ("Virgin", (649-width, 630), 90, 28), ("Verizon", (653-3*width, 644), 90, 28), ("AT&T", (650-4*width, 625), 90, 32),
        ("CA Technologies", (660-6*width, 654), 90, 15), ("Accenture", (653-7*width, 644), 90, 22), ("IBM", (650-9*width, 611), 90, 32),
        ("Alphabet", (9, 646-width), 0, 24), ("Apple", (17, 631-2*width), 0, 28), ("Microsoft", (5, 648-4*width), 0, 24),
        ("AMD", (21, 627-7*width), 0, 32), ("Intel", (22, 626-9*width), 0, 32),
        ("Holiday", (660, -7), 0, 28), ("START", (11, 48), 0, 40), ("Parking", (658, 710), 0, 28),

        ("Treasure", (114+width, 7), 270, 26), ("TAX", (113+3*width, 7), 270, 26), ("RISK", (113+6*width, 7), 270, 26),
        ("RISK", (655-2*width, 605), 90, 26), ("Treasure", (12, 647-3*width), 0, 26), ("RISK", (38, 624-6*width),0, 26),
        ("LUXURY TAX", (13, 654-8*width), 0, 20),("Treasure", (657, 647-3*width), 0, 26),

        ("Qualcomm", (651, 130+width), 0, 24), ("Foxconn", (130+width, 630), 90, 24),

        ("Spotify", (116+4*width, 10), 270, 26), ("Uber", (677, 106+4*width), 0, 26),
        ("Netflix", (129+4*width, 620), 90, 26), ("Airbnb", (20, 116+4*width), 0, 26)]

for i in range(len(data)):
    if i < 22:
        color = (148, 0, 211)
    elif 21 < i < 25:
        color = (0, 0, 0)
    elif 24 < i < 33:
        color = (0, 255, 0)
    elif 32 < i < 35:
        color = (255, 0, 0)
    else:
        color = (0, 0, 255)
        
    font = ImageFont.truetype("fonts/font1.otf", data[i][3])
    txt = Image.new('L', (100, 55))
    draw = ImageDraw.Draw(txt)
    draw.text((0,0), data[i][0], font=font, fill=255, anchor='center')
    w, h = draw.textsize(data[i][0], font=font)
    write = txt.rotate(data[i][2], expand=1)
    extraW = (58 - h)//2
    extraH = (104-w)//2
    print(extraW, extraH)
    image.paste(ImageOps.colorize(ImageOps.invert(write), color, color), (data[i][1][0], data[i][1][1]+extraH), write)

font =ImageFont.truetype("fonts/font1.otf", 96)
txt = Image.new('L', (450, 200))
draw =ImageDraw.Draw(txt)
draw.text((0,0), "MONOPOLY", font=font, fill=255, anchor='center')
image.paste(ImageOps.colorize(txt, color, (0,0,0)), (160, 320), txt)

imgs = ("police.jpg", "arrow.jpg", "parking.jpg", "holiday.jpg")
resize = ((115, 115), (100, 50), (115, 68), (80, 74))
paste_pos = ((2, 643), (9,1), (642, 652), (665, 40))

for i in range(len(imgs)):
    image2 = Image.open(imgs[i])
    image2 = image2.resize(resize[i])
    image.paste(image2, paste_pos[i])

image.save('image-drawn.png')
