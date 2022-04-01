import React from "react";
import FolderList from "../documents/FolderList";
import DocumentList from "../documents/DocumentList";

import folders from "../documents/test-data";

function Main(props) {
    return (
        <>
            <FolderList parentFolder={props.parentFolder} folders={folders}/>
            <DocumentList parentFolder={props.parentFolder}/>
        </>
    );
}

export default Main;