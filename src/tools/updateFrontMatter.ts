import * as fs from 'fs';
import * as path from 'path';
import yargs from 'yargs';
import matter from 'gray-matter';
import yaml from 'js-yaml';

const params = getParameters();
var files = findFilesInDir(params.path, ".md");

files.forEach(filePath => {
    console.log("Processing file: ", filePath);
    const oldFrontMatter = getFrontMatter(filePath);
    const newFrontMatter = processFrontMatter(oldFrontMatter, filePath, params.domain);
    overrideFrontMatter(filePath, newFrontMatter)

});

function processFrontMatter(frontMatter: FrontMatterType, filePath: string, domain: string): FrontMatterType {
    var properUrl = getUrlFromFilePath(filePath);
    var currentUrl = frontMatter["url"];

    if (currentUrl === undefined) {
        frontMatter["url"] = properUrl;
    }
    else if (currentUrl !== properUrl) {
        frontMatter["url"] = properUrl;
        var aliases = frontMatter["aliases"] || [];
        aliases.push(currentUrl);
        // Remove duplicates
        aliases = aliases.filter((value, index, self) => self.indexOf(value) === index);
        frontMatter["aliases"] = aliases;
    }    
    frontMatter["disqus_title"] = frontMatter["title"]
    frontMatter["disqus_url"] = `${domain}/${frontMatter["url"]}`
    if (frontMatter["disqus_identifier"] === undefined) {
        frontMatter["disqus_identifier"] = `${frontMatter["url"]}`
    }
    return frontMatter;
}

function getUrlFromFilePath(filePath: string): string {
    var prefixToSearch = "\\content\\english\\";
    var contentIndex = filePath.indexOf(prefixToSearch);
    var url = filePath.substring(contentIndex + prefixToSearch.length, filePath.length - 3);

    console.log("url: ", url);
    url = url.replace(/\\/g, "/");
    url = url.replace(new RegExp("\\w{4}/\\d{4}/\\d{4}-\\d{2}-\\d{2}-"), "");
    url = url.lastIndexOf("/") === -1 ?? url ? url : url.substring(url.lastIndexOf("/") + 1);
    console.log("url AFTER CHANGE: ", url);
    url = url.toLowerCase();
    return url;
}

function getParameters(): ScriptParams {
    const argv: ScriptParams = yargs(process.argv).argv;
    if (!argv.path) {
        throw new Error("Path is required");
    }
    return argv
}

type ScriptParams = {
    path: string;
    domain: string
}

function findFilesInDir(startPath: string, extension: string): string[] {
    let results: string[] = [];

    if (!fs.existsSync(startPath)) {
        console.log("Directory not found: ", startPath);
        return results;
    }

    const files = fs.readdirSync(startPath);

    for (let i = 0; i < files.length; i++) {
        const filename = path.join(startPath, files[i]);
        const stat = fs.lstatSync(filename);

        if (stat.isDirectory()) {
            const filesINDeeperDir = findFilesInDir(filename, extension); // recurse
            results = results.concat(filesINDeeperDir);
        } else if (filename.endsWith(extension)) {
            if (filename.indexOf("_index.md") == -1) {
                results.push(filename);
            }
        }
    }

    return results;
}

function getFrontMatter(filePath: string) {
    const fileContent = fs.readFileSync(filePath, 'utf8');
    const { data } = matter(fileContent);
    return data;
}

function overrideFrontMatter(filePath: string, newFrontMatter: object) {
    const fileContent = fs.readFileSync(filePath, 'utf8');
    const { content } = matter(fileContent);
    const yamlFrontMatter = yaml.dump(newFrontMatter, { sortKeys: true });
    const newFileContent = `---\n${yamlFrontMatter}---\n${content}`;
    fs.writeFileSync(filePath, newFileContent);
}

type FrontMatterType = { [key: string]: any };