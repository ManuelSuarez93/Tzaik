//to 1d functions

//get a scalar random value from a 3d value
float rand3dTo1d(float3 value, float3 dotDir = float3(12.9898, 78.233, 37.719))
{
    //make value smaller to avoid artefacts
    float3 smallValue = sin(value);
    //get scalar value from 3d vector
    float random = dot(smallValue, dotDir);
    //make value more random by making it bigger and then taking the factional part
    random = frac(sin(random) * 143758.5453);
    return random;
}

float rand2dTo1d(float2 value, float2 dotDir = float2(12.9898, 78.233))
{
    return frac(sin(dot(sin(value), dotDir)) * 143758.5453);
}

float rand1dTo1d(float3 value, float mutator = 0.546, float multiplier = 143758.5453)
{     
    return frac(sin(value + mutator) * multiplier);
}          
//to 2d functions

float2 rand3dTo2d(float3 value)
{
    return float2(
        rand3dTo1d(value, float3(12.989, 78.233, 37.719)),
        rand3dTo1d(value, float3(39.346, 11.135, 83.155))
    );
}

float2 rand2dTo2d(float2 value)
{
    return float2(
        rand2dTo1d(value, float2(12.989, 78.233)),
        rand2dTo1d(value, float2(39.346, 11.135))
    );
}

float2 rand1dTo2d(float value)
{
    return float2(
        rand2dTo1d(value, 3.9812),
        rand2dTo1d(value, 7.1536)
    );
}

//to 3d functions

float3 rand3dTo3d(float3 value)
{
    return float3(
        rand3dTo1d(value, float3(12.989, 78.233, 37.719)),
        rand3dTo1d(value, float3(39.346, 11.135, 83.155)),
        rand3dTo1d(value, float3(73.156, 52.235, 09.151))
    );
}

float3 rand2dTo3d(float2 value)
{
    return float3(
        rand2dTo1d(value, float2(12.989, 78.233)),
        rand2dTo1d(value, float2(39.346, 11.135)),
        rand2dTo1d(value, float2(73.156, 52.235))
    );
}

float3 rand1dTo3d(float value){
    return float3(
        rand1dTo1d(value, 3.9812),
        rand1dTo1d(value, 7.1536),
        rand1dTo1d(value, 5.7241)
    );
}

//VALUE NOISE
         
inline float easeIn(float interpolator)
{
    return interpolator * interpolator;
}

float easeOut(float interpolator)
{
    return 1 - easeIn(1 - interpolator);
}

float easeInOut(float interpolator)
{                     
    return lerp(easeIn(interpolator),easeOut(interpolator), interpolator);
}


float Line(float value, float worldy)
{                                                              
    return smoothstep(2 * fwidth(worldy), fwidth(worldy), abs(value - worldy));
}
float ValueNoise2d(float2 value)
{
    float upperLeftCell = rand2dTo1d(float2(floor(value.x), ceil(value.y)));
    float upperRightCell = rand2dTo1d(float2(ceil(value.x), ceil(value.y)));
    float lowerLeftCell = rand2dTo1d(float2(floor(value.x), floor(value.y)));
    float lowerRightCell = rand2dTo1d(float2(ceil(value.x), floor(value.y)));

    float interpolatorX = easeInOut(frac(value.x));
    float interpolatorY = easeInOut(frac(value.y));

    float upperCells = lerp(upperLeftCell, upperRightCell, interpolatorX);
    float lowerCells = lerp(lowerLeftCell, lowerRightCell, interpolatorX);

    float noise = lerp(lowerCells, upperCells, interpolatorY);
    return noise;
}

float3 ValueNoise3d(float3 value)
{            
    float3 cellNoiseZ[2];
    [unroll]
    for (int z = 0; z <= 1; z++)
    {
        float3 cellNoiseY[2];
        [unroll]
        for (int y = 0; y <= 1; y++)
        {
            float3 cellNoiseX[2];
            [unroll]
            for (int x = 0; x <= 1; x++)
            {
                float3 cell = floor(value) + float3(x, y, z);
                
                cellNoiseX[x] = rand3dTo3d(cell);
            }  
            cellNoiseY[y] = lerp(cellNoiseX[0], cellNoiseX[1], easeInOut(frac(value.x)));
        }
        cellNoiseZ[z] = lerp(cellNoiseY[0], cellNoiseY[1], easeInOut(frac(value.y)));
    }              
    
    return lerp(cellNoiseZ[0], cellNoiseZ[1], easeInOut(frac(value.z)));

}

//PERLIN NOISE
float gradientNoise(float value)
{                                                       
    return lerp(rand1dTo1d(floor(value)) * 2 - 1 * frac(value),
                rand1dTo1d(ceil(value)) * 2 - 1 * (frac(value) - 1), easeInOut(frac(value)));
}

float perlinNoise2d(float2 value)
{           
    return lerp(
                lerp(
                    dot(rand2dTo2d(float2(floor(value.x), floor(value.y))) * 2 - 1,
                    frac(value) - float2(0, 0)),
                    dot(rand2dTo2d(float2(ceil(value.x), floor(value.y))) * 2 - 1,
                    frac(value) - float2(0, 1)),
                    easeInOut(frac(value).x)),
                lerp(
                    dot(rand2dTo2d(float2(floor(value.x), ceil(value.y))) * 2 - 1,
                    frac(value) - float2(1, 0)),
                    dot(rand2dTo2d(float2(ceil(value.x), ceil(value.y))) * 2 - 1,
                    frac(value) - float2(1, 1)),
                    easeInOut(frac(value).x)),
            easeInOut(frac(value).y));
}

float3 perlinNoise3d(float3 value)
{
    float3 cellNoiseZ[2];
    [unroll]
    for (int z = 0; z <= 1; z++)
    {
        float3 cellNoiseY[2];
        [unroll]
        for (int y = 0; y <= 1; y++)
        {
            float3 cellNoiseX[2];
            [unroll]
            for (int x = 0; x <= 1; x++)
            {
                float3 cell = floor(value) + float3(x, y, z);
                float3 cellDirection = rand3dTo3d(cell) * 2 - 1;
                float3 compareVector = frac(value) - float3(x, y, z);
                cellNoiseX[x] = dot(cellDirection, compareVector);
            }
            cellNoiseY[y] = lerp(cellNoiseX[0], cellNoiseX[1], easeInOut(frac(value).x));
        }
        cellNoiseZ[z] = lerp(cellNoiseY[0], cellNoiseY[1], easeInOut(frac(value).y));
    }      
    
    return lerp(cellNoiseZ[0], cellNoiseZ[1], easeInOut(frac(value).z));
}

float wavenoise(float3 value)
{
    float noise = frac((perlinNoise3d(value) + 0.5) * 6);
    return smoothstep(1 - fwidth(noise), 1, noise) + smoothstep(fwidth(noise), 0, noise);
}
 
float layeredNoise1d(float value, int octaves, float persistance, float roughness)
{          
    float noise = 0;
    float frequency = 1;
    float factor = 1;
    
    for (int i = 0; i < octaves; i++)
    {
        noise += gradientNoise(value * frequency + i * 0.72354) * factor;
        factor *= persistance;
        frequency *= roughness;
    }
    
    return noise;
}

float layeredNoise2d(float2 value, int octaves, float persistance, float roughness)
{
    float noise = 0;
    float frequency = 1;
    float factor = 1;
    
    for (int i = 0; i < octaves; i++)
    {
        noise += perlinNoise2d(value * frequency + i * 0.72354) * factor;
        factor *= persistance;
        frequency *= roughness;
    }
    
    return noise;
}

float3 layeredNoise3d(float3 value, int octaves, float persistance, float roughness)
{
    float noise = 0;
    float frequency = 1;
    float factor = 1;
            
    [unroll]
    for (int i = 0; i < octaves; i++)
    {
        noise += perlinNoise3d(value * frequency + i * 0.72354) * factor;
        factor *= persistance;
        frequency *= roughness;
    }
    
    return noise;
}

float3 voronoiNoise(float2 value)
{     
    float minDistToCell = 10;
    float2 baseCell = floor(value);
    float2 closestCell;
    float2 toClosestCell;
    [unroll]
    for(int x=-1; x<=1; x++)
    {
        [unroll]
        for (int y = -1; y <= 1; y++)
        {
            float2 cell = baseCell + float2(x, y);
            float2 cellPosition = cell + rand2dTo2d(cell);
            float2 toCell = cellPosition - value;
            float distToCell = length(toCell);
            if (distToCell < minDistToCell)
            {
                minDistToCell = distToCell;
                closestCell = cell;
                toClosestCell = toCell;
            }
        }
    }
    
    float minEdgeDistance = 10;
    [unroll]
    for (int x2 = -1; x2 <= 1; x2++)
    {
        [unroll]    
        for (int y2 = -1; y2 <= 1; y2++)
        {
            float2 cell = baseCell + float2(x2, y2);
            float2 cellPosition = cell + rand2dTo2d(cell);
            float2 toCell = cellPosition - value;

            float2 diffToClosestCell = abs(closestCell - cell);
            bool isClosestCell = diffToClosestCell.x + diffToClosestCell.y < 0.1;
            if (!isClosestCell)
            {                                    
                minEdgeDistance = min(minEdgeDistance, dot((toClosestCell + toCell) * 0.5, normalize(toCell - toClosestCell)));
            }
        }
    }     
    
    return float3(minDistToCell, rand2dTo1d(closestCell), minEdgeDistance);   
}

float3 voronoiNoise3d(float3 value)
{
    float3 baseCell = floor(value);

    //first pass to find the closest cell
    float minDistToCell = 10;
    float3 toClosestCell;
    float3 closestCell;
    [unroll]
    for (int x1 = -1; x1 <= 1; x1++)
    {
        [unroll]
        for (int y1 = -1; y1 <= 1; y1++)
        {
            [unroll]
            for (int z1 = -1; z1 <= 1; z1++)
            {
                float3 cell = baseCell + float3(x1, y1, z1);
                float3 cellPosition = cell + rand3dTo3d(cell);
                float3 toCell = cellPosition - value;
                float distToCell = length(toCell);
                if (distToCell < minDistToCell)
                {
                    minDistToCell = distToCell;
                    closestCell = cell;
                    toClosestCell = toCell;
                }
            }
        }
    }

    //second pass to find the distance to the closest edge
    float minEdgeDistance = 10;
    [unroll]
    for (int x2 = -1; x2 <= 1; x2++)
    {
        [unroll]
        for (int y2 = -1; y2 <= 1; y2++)
        {
            [unroll]
            for (int z2 = -1; z2 <= 1; z2++)
            {
                float3 cell = baseCell + float3(x2, y2, z2);
                float3 cellPosition = cell + rand3dTo3d(cell);
                float3 toCell = cellPosition - value;

                float3 diffToClosestCell = abs(closestCell - cell);
                bool isClosestCell = diffToClosestCell.x + diffToClosestCell.y + diffToClosestCell.z < 0.1;
                if (!isClosestCell)
                {
                    float3 toCenter = (toClosestCell + toCell) * 0.5;
                    float3 cellDifference = normalize(toCell - toClosestCell);
                    float edgeDistance = dot(toCenter, cellDifference);
                    minEdgeDistance = min(minEdgeDistance, edgeDistance);
                }
            }
        }
    }

    float random = rand3dTo1d(closestCell);
    return float3(minDistToCell, random, minEdgeDistance);
}

float3 voronoiNoiseWithEdges(float2 value, float3 borderColor)
{
    return lerp(rand1dTo3d(voronoiNoise(value).y), 
        borderColor, 
        1 - smoothstep(0.05 - length(fwidth(value)), 0.05 + length(fwidth(value)), voronoiNoise(value).z));
}

float3 voronoiNoiseWithEdges3d(float3 value, float3 borderColor)
{
    return lerp(rand1dTo3d(voronoiNoise3d(value).y),
        borderColor,
        1 - smoothstep(0.05 - length(fwidth(value)), 0.05 + length(fwidth(value)), voronoiNoise3d(value).z));
}
//FRESNEL
float3 fresnelColor(float3 worldNormal, float3 viewDir, float fresnelExponent, float3 fresnelColor)
{
    float fresnel = dot(worldNormal, viewDir);
    fresnel = saturate(1 - fresnel);
    fresnel = pow(fresnel, fresnelExponent);  
    return fresnel * fresnelColor;
}

//DITHER
float3 dither(float red, float green, float blue, float2 screenPos, sampler2D pattern, float3 screenParams, float3 texelSize)
{
    float2 ditherCoordinate = screenPos * screenParams.xy * texelSize.xy; 
    float ditheredValue1 = step(tex2D(pattern, ditherCoordinate).r, red);
    float ditheredValue2 = step(tex2D(pattern, ditherCoordinate).g, green);
    float ditheredValue3 = step(tex2D(pattern, ditherCoordinate).b, blue);

    return float3(ditheredValue1, ditheredValue2, ditheredValue3);
}
   
//TOON SHADER   
float getShadow(float shadowAttenuation)
{
#ifdef USING_DIRECTIONAL_LIGHT
	return smoothstep(0.5 - (fwidth(shadowAttenuation) * 0.5f), 0.5 + (fwidth(shadowAttenuation) *0.5f), shadowAttenuation);
#else								 
    return smoothstep(0, fwidth(shadowAttenuation), shadowAttenuation);
#endif
				 
}

float4 calculateToonLight(float3 normal, float3 lightDir, half3 viewDir, float shadowAttenuation, 
float stepWidth, float stepAmount)
{
    float towardsLight = dot(normal, lightDir);
    towardsLight = towardsLight / stepWidth;

    float lightIntensity = floor(towardsLight);
    float change = fwidth(towardsLight);
    float smoothing = smoothstep(0, change, frac(towardsLight));

    lightIntensity = lightIntensity / stepAmount;
    lightIntensity = saturate(lightIntensity);

    return lightIntensity * getShadow(shadowAttenuation);
}

float4 calculateSpecular(float3 normal, float3 lightDir, half3 viewDir, 
float shadowAttenuation, float falloff, float size)
{
    float3 reflectionDirection = reflect(lightDir, normal);
    float towardsReflection = dot(viewDir, -reflectionDirection);

    float specularFalloff = dot(viewDir, normal);
    specularFalloff = pow(specularFalloff, falloff);
    towardsReflection = towardsReflection * specularFalloff;

    float specularChange = fwidth(towardsReflection);
    float specularIntensity = smoothstep(1 - size, 1 - size + specularChange, towardsReflection);
    return specularIntensity * getShadow(shadowAttenuation);
}
