import * as fs from 'fs';
import * as path from 'path';
import yargs from 'yargs';
import matter from 'gray-matter';
import yaml from 'js-yaml';

const params = getParameters();
var files = findFilesInDir(params.path, ".md");

files.forEach(filePath => {
    console.log("Processing file: ", filePath);
    const frontMatter = getFrontMatter(filePath);
    overrideFrontMatter(filePath, frontMatter)

});

function getParameters(): ScriptParams {
    const argv: ScriptParams = yargs(process.argv).argv;
    if (!argv.path) {
        throw new Error("Path is required");
    }
    return argv
}

type ScriptParams = {
    path: string;
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
            results.push(filename);
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