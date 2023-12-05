using AoC_Toolbox;
using AoC_Toolbox.Geometry;
using AoC_Toolbox.InputParsing;
using AoC_Toolbox.Pathfinding;
using System.Collections.Generic;
using System.ComponentModel;

internal class Solution
{
    static void Main(string[] args)
    {

        List<(long start, long end)> list1 = new List<(long start, long end)>()
        { 
            (15, 25), (26, 35), (50, 55),(58,59)
            //(15, 25)
        };

        List<(long start, long end)> list2 = new List<(long start, long end)>()
        {
            (5,16), (24,27), (53, 60)
           // (15, 20), (19, 35), (50, 55)
            //(15, 20)
        };


        var split = SplitRegions(list1, list2);

        Console.WriteLine(string.Join("\n", split.Select(x => $"{x.start} {x.end}")));


        PuzzleSetup.Solve(solutionPart1, solutionPart2);
    }

    static List<long> seedsAsValues;
    static List<(long startDest, long endDest, long startSrc, long endSrc)> seedToSoilMap;
    static List<(long startDest, long endDest, long startSrc, long endSrc)> soilToFertilizerMap;
    static List<(long startDest, long endDest, long startSrc, long endSrc)> fertilizerToWaterMap;
    static List<(long startDest, long endDest, long startSrc, long endSrc)> waterToLightMap;
    static List<(long startDest, long endDest, long startSrc, long endSrc)> lightToTemperatureMap;
    static List<(long startDest, long endDest, long startSrc, long endSrc)> temperatureToHumidityMap;
    static List<(long startDest, long endDest, long startSrc, long endSrc)> humidityToLocationMap;

    static void parseInput(string[] input)
    {
        seedsAsValues = new();
        seedToSoilMap = new();
        soilToFertilizerMap = new();
        fertilizerToWaterMap = new();
        waterToLightMap = new();
        lightToTemperatureMap = new();
        temperatureToHumidityMap = new();
        humidityToLocationMap = new();

        seedsAsValues = input[0].Split(": ")[1]
                                .Split()
                                .Select(long.Parse)
                                .ToList();

        var parserState = 0;

        foreach (var line in input)
        {
            parserState = line switch
            {
                "seed-to-soil map:" => 1,
                "soil-to-fertilizer map:" => 2,
                "fertilizer-to-water map:" => 3,
                "water-to-light map:" => 4,
                "light-to-temperature map:" => 5,
                "temperature-to-humidity map:" => 6,
                "humidity-to-location map:" => 7,
                _ => parserState
            };

            if (line.Contains(':') || line == "")
            {
                continue;
            }

            var value = GetRangeFromInput(line);

            switch (parserState)
            {
                case 1:
                    seedToSoilMap.Add(value);
                    break;

                case 2:
                    soilToFertilizerMap.Add(value);
                    break;

                case 3:
                    fertilizerToWaterMap.Add(value);
                    break;

                case 4:
                    waterToLightMap.Add(value);
                    break;

                case 5:
                    lightToTemperatureMap.Add(value);
                    break;

                case 6:
                    temperatureToHumidityMap.Add(value);
                    break;

                case 7:
                    humidityToLocationMap.Add(value);
                    break;
            }
        }
    }

    static object? solutionPart1(string[] input)
    {
        parseInput(input);

        var minimumLocation = long.MaxValue;

        foreach(var seed in seedsAsValues)
        {
            var soil = GetDestinationValue(seedToSoilMap, seed);
            var fertilizer = GetDestinationValue(soilToFertilizerMap, soil);
            var water = GetDestinationValue(fertilizerToWaterMap, fertilizer);
            var light = GetDestinationValue(waterToLightMap, water);
            var temperature = GetDestinationValue(lightToTemperatureMap, light);
            var humidity = GetDestinationValue(temperatureToHumidityMap, temperature);
            var location = GetDestinationValue(humidityToLocationMap, humidity);

            minimumLocation = Math.Min(minimumLocation, location);
        }

        return minimumLocation;
    }


    static object? solutionPart2(string[] input)
    {
        parseInput(input);

        var seedAsRanges = seedsAsValues.Chunk(2)
                                        .Select(c => (start: c[0], end: c[0] + c[1] - 1))
                                        .ToList();

        var next = GetNextRegion(seedAsRanges, seedToSoilMap);    
        next = GetNextRegion(next, soilToFertilizerMap);
        next = GetNextRegion(next, fertilizerToWaterMap);
        next = GetNextRegion(next, waterToLightMap);
        next = GetNextRegion(next, lightToTemperatureMap);
        next = GetNextRegion(next, temperatureToHumidityMap);
        next = GetNextRegion(next, humidityToLocationMap);

        return next.Min(x => x.start);
    }


    static (long startDest, long endDest, long startSrc, long endSrc) GetRangeFromInput(string input)
    {
        var values = input.Split()
                            .Select(long.Parse)
                            .ToList();

        return (values[0], values[0] + values[2] - 1, values[1], values[1] + values[2] - 1);
    }

    static long GetDestinationValue(List<(long startDest, long endDest, long startSrc, long endSrc)> map, long source)
    {
        var destinationMap = map.FirstOrDefault(x => source >= x.startSrc && source <= x.endSrc,
                                                (startDest: source, endDest: source, startSrc: source, endSrc: source));

        var destination = (source - destinationMap.startSrc) + destinationMap.startDest;

        return destination;
    }

    static List<(long start, long end)> GetNextRegion(List<(long start, long end)> initialRegions, List<(long startDest, long endDest, long startSrc, long endSrc)> map)
    {
        var checkDestinations = SplitRegions(
            initialRegions,
            map.Select(m => (start: m.startSrc, end: m.endSrc)).ToList()
            );

        
        var nextRegion = new List<(long start, long end)>();

        foreach (var checkRange in checkDestinations)
        {
            var startDest = GetDestinationValue(map, checkRange.start);
            var endDest = GetDestinationValue(map, checkRange.end);

            nextRegion.Add((startDest, endDest));
        }

        return nextRegion;
    }


    static List<(long start, long end)> SplitRegions(List<(long start, long end)> list1, List<(long start, long end)> list2)
    {
        var result = new List<(long start, long end)>();

        foreach (var region in list1.OrderBy(x => x.start))
        {
            var start = region.start;

            foreach (var border in list2.OrderBy(x => x.start))
            {
                if (border.end < region.start)
                {
                    continue;
                }

                if (border.end >= region.end)
                {
                    result.Add((start, region.end));
                    start = region.end;
                    break;
                }


                if (border.start > region.start && border.start <= region.end && border.start != start)
                {
                    result.Add((start, border.start - 1));

                    start = border.start;
                }

                if (border.end < region.end && border.end >= region.start && border.end != start)
                {
                    result.Add((start, border.end));

                    start = border.end + 1;
                }

                if (border.start > region.start && border.end < region.end)
                {
                    result.Add((border.start, border.end));

                    start = border.end + 1;
                }
            }

            result.Add((start, region.end));
        }

        return result.ToList();
    }
}