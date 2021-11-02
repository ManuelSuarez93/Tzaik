float getShadow(float shadowAttenuation)
{
	#ifdef USING_DIRECTIONAL_LIGHT
		return smoothstep(0.5 - (fwidth(shadowAttenuation) * 0.5f), 0.5 + (fwidth(shadowAttenuation) *0.5f), shadowAttenuation);
	#else								 
		return smoothstep(0, fwidth(shadowAttenuation), shadowAttenuation);
	#endif
				 
}

float4 calculateToonLight(float3 normal, float3 lightDir, half3 viewDir, float shadowAttenuation, float stepWidth, float stepAmount)
{
	//CALCULATE LIGHTS
	float towardsLight = dot(normal, lightDir);
	towardsLight = towardsLight / stepWidth;

	float lightIntensity = floor(towardsLight);
	float change = fwidth(towardsLight);
	float smoothing = smoothstep(0, change, frac(towardsLight));

	lightIntensity = lightIntensity / stepAmount;
	lightIntensity = saturate(lightIntensity);

	return lightIntensity * getShadow(shadowAttenuation);
}	

float4 calculateSpecular(float3 normal, float3 lightDir, half3 viewDir, float shadowAttenuation)
{
	float3 reflectionDirection = reflect(lightDir, normal);
	float towardsReflection = dot(viewDir, -reflectionDirection);

	float specularFalloff = dot(viewDir, normal);
	specularFalloff = pow(specularFalloff, _SpecularFalloff);
	towardsReflection = towardsReflection * specularFalloff;

	

	float specularChange = fwidth(towardsReflection);
	float specularIntensity = smoothstep(1 - _SpecularSize, 1 - _SpecularSize + specularChange, towardsReflection);
	return specularIntensity * getShadow(shadowAttenuation);
}	
		   
float4 LightingStepped(float3 normal, float3 lightDir, half3 viewDir, float shadowAttenuation, float stepWidth, float stepAmount)
{	    
	float4 color;
	color.rgb = s.Albedo * calculateToonLight(normal,lightDir, viewDir, shadowAttenuation, stepWidth, stepAmount) * _LightColor0.rgb;
	color.rgb = lerp(color.rgb, s.Specular * _LightColor0.rgb, saturate(calculateSpecular(normal, lightDir, viewDir, shadowAttenuation)));
	color.a = s.Alpha;
	return color;
}

		
float3 fresnelColor(float3 worldNormal, float3 viewDir)
{
	float fresnel = dot(worldNormal, viewDir);
	fresnel = saturate(1 - fresnel);
	fresnel = pow(fresnel, _FresnelExponent);
	float3 fresnelColor = fresnel * _FresnelColor;
	return fresnelColor;
}	   
	
float3 dither(float red, float green, float blue, float2 screenPos, sampler2D pattern)
{
	float2 ditherCoordinate = screenPos * _ScreenParams.xy * _DitherPattern_TexelSize.xy;
	float ditherValue = tex2D(pattern, ditherCoordinate).r;
	float ditheredValue1 = step(tex2D(pattern, ditherCoordinate).r, red);
	float ditheredValue2 = step(tex2D(pattern, ditherCoordinate).g, green);
	float ditheredValue3 = step(tex2D(pattern, ditherCoordinate).b, blue);

	return float3(ditheredValue1, ditheredValue2, ditheredValue3);
}