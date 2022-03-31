import React from "react";

function FolderList(props) {
    return (
        <section>
            <h3>Folders available under the {props.parentFolder} folder</h3>
            <ul>
                {props.folders.map(folder => (
                    <li key={folder.id}>
                        <a href={`#${folder.id}`}>{folder.name}</a>
                    </li>
                ))}
            </ul>
        </section>
    );
}

export default FolderList;