import React from "react";

function FolderList(props) {
    const displayFolders = props.folders.filter(fold => fold.parentPath === props.parentFolder);
    return (
        <section>
            <h3>Folders available under the {props.parentFolder || "/"} folder</h3>
            <ul>
                {displayFolders.map((folder, i) => (
                    <li key={folder.fullPath}>
                        <a href={`#${folder.fullPath}`}>{folder.name}</a>
                    </li>
                ))}
            </ul>
        </section>
    );
}

export default FolderList;