import React from 'react';

function DocumentList(props) {
  return (
      <section>
        <h3>Documents available under the {props.parentFolder} folder:</h3>
      </section>
  );
}

export default DocumentList;