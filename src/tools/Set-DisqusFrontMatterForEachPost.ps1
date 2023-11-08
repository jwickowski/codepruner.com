
 
 #Install-Module -Name PowerShell-Yaml

#  param (
#         [Parameter(Mandatory)]
#         [string] 
#         $path
#        )

     

function Add-DisqusDataToFrontMatter {
    param (
        [Parameter(Mandatory)]
         [string] 
         $Path
    )

    }


    function Add-DisqusDataToFrontMatterForOneFile {
    param (
        [Parameter(Mandatory)]
         [string] 
         $FilePath
    )
    $frontMatterData = Parse-FrontMatter -Path $FilePath
    ## manipulate, add remove
    Update-FrontMAtter -Path $FilePath -frontMatter $frontMatterData
   $urlExists = $false;
   $url = "";
    
    (Get-Content $FilePath) | 
    Foreach-Object {
        if ($_ -match "url:") 
        {
              $url = $_.Replace("url:", "").Replace('"','').Trim();
            $urlExists = $true;
            #Add Lines after the selected pattern 
            #"url: ""$url"""
            #"images:"
            #"  - ""images/blog/red-sharp-pruner.jpg"""
            #"author: ""Jerzy Wickowski"""
           # "categories: [""$category""]"
            
        }
    }

    if($url -eq ""){

    $url = $FilePath.Replace("C:\Projects\jw\codepruner.com\src\codepruner.com\content\","")
    $url = $url.Replace(".md","")
    $url = $url.Replace("\","/")
        }


    #Write-Host $category

   
   
   return
   
    (Get-Content $fileName) | 
    Foreach-Object {
        $_ # send the current line to output
        if ($_ -match "categories:") 
        {
            #Add Lines after the selected pattern 
            #"url: ""$url"""
            #"images:"
            #"  - ""images/blog/red-sharp-pruner.jpg"""
            #"author: ""Jerzy Wickowski"""
           # "categories: [""$category""]"
            "type: ""regular"""
        }
    } | Set-Content $fileName


    }



$filePath = "C:\Projects\jw\codepruner.com\src\codepruner.com\content\english\post\2023\2023-09-22-how-to-run-postgresql-and-adminer-or-pgadmin-with-docker-compose.md"    
     # Add-DisqusDataToFrontMatterForOneFile -FilePath "C:\Projects\jw\codepruner.com\src\codepruner.com\content\english\post\2023\2023-09-22-how-to-run-postgresql-and-adminer-or-pgadmin-with-docker-compose.mdt"

     Get-Metadata -Path $filePath


