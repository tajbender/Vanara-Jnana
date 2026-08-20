using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;

namespace Jnana.ViewModels;

public class AzureCloudInfo
{
    public static IEnumerable<string> GetRegisteredClouds()
    {
        var clouds = new List<string>();

        foreach (AzureCloudInstance instance in Enum.GetValues(typeof(AzureCloudInstance)))
        {
            clouds.Add($"{instance} → {GetAuthority(instance)}");
        }

        return clouds;
    }

    private static string GetAuthority(AzureCloudInstance instance)
    {
        return instance switch
        {
            AzureCloudInstance.AzurePublic => "https://login.microsoftonline.com",
            AzureCloudInstance.AzureChina => "https://login.chinacloudapi.cn",
            AzureCloudInstance.AzureUsGovernment => "https://login.microsoftonline.us",
            AzureCloudInstance.AzureGermany => "https://login.microsoftonline.de",
            _ => "unknown"
        };
    }
}
