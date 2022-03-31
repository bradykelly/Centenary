import React from "react";
import FolderList from "../documents/FolderList";
import DocumentList from "../documents/DocumentList";

const folders = [
    {
        fullPath: "Folder 1/Folder 1 - A/Folder C",
        name: "Folder 1 - A",
        parentPath: "Folder 1",
    },
];

function Main(props) {
    const parentFolder = props.parentFolder || "/";
    return (
        <div>
            <FolderList parentFolder={parentFolder} folders={folders}/>
            <DocumentList parentFolder={parentFolder}/>
        </div>
    );
}

export default Main;