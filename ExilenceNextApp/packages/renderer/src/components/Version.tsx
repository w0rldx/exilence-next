import React from 'react';

import { versions } from '#preload';

interface VersionProp {
  versions: {
    [key: string]: string;
  };
}

export const Version = () => {
  const versionsComponents = () => {
    if (!versions) {
      return <div>no versions</div>;
    }

    const castedVersions = versions as VersionProp;

    return Object.keys(castedVersions.versions).map((key) => {
      return (
        <tr key={key}>
          <td>{key}</td>
          <td>{castedVersions.versions[key]}</td>
        </tr>
      );
    });
  };

  return (
    <div>
      <table>
        <thead>
          <tr>
            <th>Package</th>
            <th>Version</th>
          </tr>
        </thead>
        <tbody>{versionsComponents()}</tbody>
      </table>
    </div>
  );
};
